#include <ESP8266WiFi.h>
#include <WebSocketsServer.h>
#include <WiFiClientSecure.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include <LiquidCrystal_I2C.h>
#include <Adafruit_NeoPixel.h>
#include <Wire.h>
#include <SoftwareSerial.h>

int RX = 5;  // Example: D5 pin
int TX = 2;  // Example: D6 pin
SoftwareSerial mySerial(RX, TX);

// WiFi settings
const char* ssid = "CE-Hub-Student";
const char* password = "casa-ce-gagarin-public-service";

const char* apiToken = "200950623fa417ec39096a0a6c9805283019c380";

// WebSocket server
WebSocketsServer webSocket = WebSocketsServer(81);

// LED strip settings
#define LED_PIN    13 // Replace with the pin connected to the LED strip
#define NUM_LEDS   60
Adafruit_NeoPixel strip = Adafruit_NeoPixel(NUM_LEDS, LED_PIN, NEO_GRB + NEO_KHZ800);

// LCD screen settings
LiquidCrystal_I2C lcd(0x27, 16, 2); // Set the I2C address to 0x27, 16 columns and 2 rows

void setup() {
  Serial.begin(9600);
  mySerial.begin(9600);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }
  Serial.println("Connected to WiFi");

  lcd.init();
  lcd.backlight();
  Serial.println("Lcd start.");

  // Initialize the LED strip
  strip.begin();
  for (int i = 0; i < NUM_LEDS; i++) {
    strip.setPixelColor(i, strip.Color(25, 25, 25)); // Set to white color
  }
  strip.show(); // Update LED strip to show colors

  webSocket.begin();
  webSocket.onEvent(webSocketEvent);
  Serial.println("WebSocket server started");
}

void loop() {
  webSocket.loop();
}

void webSocketEvent(uint8_t num, WStype_t type, uint8_t* payload, size_t length) {
  if (type == WStype_TEXT) {
    String message = String((char*)payload);
    handleWebSocketMessage(message);
  }
}

void handleWebSocketMessage(String message) {
  int delimiterIndex = message.indexOf('-');
  String cityname = message.substring(0, delimiterIndex);
  String parameter = message.substring(delimiterIndex + 1);

  testAPICall(cityname, parameter);
}

void testAPICall(String city, String parameter) {
  WiFiClientSecure wifiClient;
  wifiClient.setInsecure();

  HTTPClient http;
  String apiUrl = "https://api.waqi.info/feed/" + city + "/?token=" + apiToken;

  http.begin(wifiClient, apiUrl);
  int httpCode = http.GET();

  if (httpCode > 0) {
    String payload = http.getString();
    Serial.println("API Response for " + city + ":");
    extractAndPrintData(payload, parameter, city);
  } else {
    Serial.print("Error on HTTP request to " + city + ": ");
    Serial.println(httpCode);
  }

  http.end();
}

void extractAndPrintData(String json, String parameter, String city) {
  const size_t capacity = JSON_ARRAY_SIZE(3) + JSON_OBJECT_SIZE(2) + JSON_OBJECT_SIZE(6) + 2*JSON_OBJECT_SIZE(8) + 3*JSON_OBJECT_SIZE(9) + JSON_OBJECT_SIZE(12) + 5000;
  DynamicJsonDocument doc(capacity);

  deserializeJson(doc, json);

  if (doc["status"] == "ok") {
    int aqi = doc["data"]["aqi"];
    Serial.print("AQI: ");
    Serial.println(aqi);

    // Display on LCD screen
    displayCityAndAQI(city, aqi);

    // Set LED strip color
    setLEDColorForAQI(aqi);

    if (doc["data"]["iaqi"].containsKey(parameter)) {
      float value = doc["data"]["iaqi"][parameter]["v"];
      Serial.print(parameter + ": ");
      Serial.println(value);
      // Send raw sensor value via serial communication
      mySerial.println(String(parameter) + ":" + String(value));
    } else {
      Serial.println("Requested parameter not found in API response");
    }
  } else {
    Serial.println("API response is not OK");
  }
}

void displayCityAndAQI(String city, int aqi) {
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print(city);
  lcd.setCursor(0, 1);
  lcd.print("AQI: " + String(aqi));
}

void setLEDColorForAQI(int aqi) {
  uint32_t color;
  if (aqi <= 50) color = strip.Color(67, 151, 106);
  else if (aqi <= 100) color = strip.Color(250, 223, 89);
  else if (aqi <= 150) color = strip.Color(241, 158, 74);
  else if (aqi <= 200) color = strip.Color(187, 39, 56);
  else if (aqi <= 300) color = strip.Color(93, 14, 147);
  else color = strip.Color(115, 20, 37);

  for (int i = 0; i < NUM_LEDS; i++) {
    strip.setPixelColor(i, color);
  }
  strip.show();
}
