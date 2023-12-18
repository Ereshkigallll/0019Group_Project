#include <Servo.h>
#include <SoftwareSerial.h>

Servo myservo;  // Create a servo object
SoftwareSerial mySerial(12, 13); // RX, TX

String incomingData;  // To store received data
String sensorType;
float sensorValue;

void setup() {
  Serial.begin(9600);  // Hardware serial for debugging
  mySerial.begin(9600); // Communication baud rate with other devices

  myservo.attach(16);  // Connect servo to the corresponding pin
}

void loop() {
  if (mySerial.available() > 0) {
    // Read data from the software serial
    incomingData = mySerial.readStringUntil('\n');

    Serial.println("Received: " + incomingData);

    int separatorPos = incomingData.indexOf(':');
    sensorType = incomingData.substring(0, separatorPos);
    sensorValue = incomingData.substring(separatorPos + 1).toFloat();

    // Calculate the servo angle based on sensor type and value
    int angle = mapSensorValueToAngle(sensorType, sensorValue);
    myservo.write(angle); // Control the servo to rotate
  }
}

int mapSensorValueToAngle(String sensorType, float sensorValue) {
  if (sensorType == "pm25") {
    return map(sensorValue, 0, 300, 0, 42);
  } else if (sensorType == "pm10") {
    return map(sensorValue, 0, 300, 42, 84);
  } else if (sensorType == "no2") {
    return map(sensorValue, 0, 300, 84, 126);
  }
  return 0; // Default angle if sensor type does not match
}
