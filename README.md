# 0019Group_Project

## 1. Introduction
Welcome to AeroScan's project repository. This project is the final project of the CASA0019 course, in which we created an AR mobile application for use on Android using Unity and the Vuforia engine. By scanning the name of a city on a map, the corresponding city landmarks will appear on the mobile screen as well as the local air quality. By tapping on a constituent, a physical meter will display the value of that constituent through a pointer while displaying the city name and AQI index on the LCD screen. The aim of the project is to help people travel healthier, for example by wearing a mask when the air quality is poor. At the same time, we want to make more people aware of the importance of protecting air quality, as it is a resource on which we all depend.

Let's start!! :tada: :tada: :tada:

## 2. Required software and hardware

### 2.1 Required Software

- Arduino IDE
- AutoDesk Fusion 360
- Figma
- Unity

### 2.2 Required Hardware

- ESP8266 $\times 2$
- Servo motor SG 90 $\times 1$
- LED strip
- 1602 LCD screen
- Wood boards
- acrylic boards
- 3D printer
- Laser cutter

## 3. Files

### 3.1 `00_Assets`
- `logo.png`: Images used as icon for mobile app
- `map.png`: This image can be printed directly as a map for AR image recognition.


### 3.2 `01_Arduino_file`
- `master`: The code is used for uploading to the ESP8266 master. The main functions include receiving data from the mobile application via web socket while displaying the information on the LCD screen, controlling the colour of the LED strip and transferring data to the ESP8266 slave.
- `slave`: The code is used for uploading to the ESP8266 slave. The main function consists of receiving information from the main unit while controlling the rotation angle of the servo motor.

### 3.3 `02_Unity_file`

Due to Github restrictions, the full Unity files, which contains the complete configuration file as well as the code. can be found [here](https://drive.google.com/file/d/1IlSQ94-pa_CAHy0EGBwvVm23tg4M9ehm/view?usp=sharing).

#### 3.3.1 `AirQualityAPIClient.cs`
This document is primarily used to get air quality information from [this site](https://aqicn.org)'s API in Unity.

:warning: Please replace the API token within the file with your own token from the website before using it.

#### 3.3.2 `BuildingController.cs`
This script is mainly used to control the appearance and disappearance of small spheres and buttons on the side of the building model that are used to display air quality information. When no city is selected, the spheres and buttons are hidden by default. When the user clicks on a landmark, the spheres and buttons for that city will appear. At the same time, the values of the corresponding air quality components will appear on the spheres. 

![Balls and buttons](https://github.com/Ereshkigallll/0019Group_Project/blob/main/03_Readme_pic/balls.jpg)

#### 3.3.3 `UIdisappear.cs`

This script is used to control the display and disappearance of the guiding statement on the screen after the app has finished initialising. In subsequent revisions, we will consider changing the control of disappearance by time to disappear after scanning the map.

#### 3.3.4 `VuforiaTouchable.cs`

This script is mainly used to control the appearance and disappearance of models and to send messages to the ESP8266 via web socket. When the user touches the landmark model of a city, the rest of the cities will disappear and the name of the city will be sent to ESP8266. When the user touches the model again, the rest of the hidden models will appear.

#### 3.3.5 `WeatherDisplay.cs`

This script is mainly used to display the data retrieved from the API on the specified TextMeshPro component. This script allows the user to select the targets, cities and air quality components to be displayed. All you need to do is change the city you want to display to cityName in Unity and tick the name of the component you want to display below.

The detailed interface can be seen in the following screenshot.

![weather display](https://github.com/Ereshkigallll/0019Group_Project/blob/main/03_Readme_pic/weather.png)

#### 3.3.6 `WeatherService.cs`

This script is mainly used to get data from the API, and at the same time will get all the data stored in the buffer and printed out for debugging.

#### 3.3.7 `WebSocketClient.cs`

This script mainly implements the function of establishing and managing WebSocket connections with the ESP8266's WebSocket server in Unity.

### 3.4 `03_Readme_pic`

Some of the images used for this Read me file, please just ignore them.

## 4. Circuit Connections

The following diagram shows the circuit connections for this project and can be used for reference when assembling the circuit.

![circuit](https://github.com/Ereshkigallll/0019Group_Project/blob/main/03_Readme_pic/circuit.png)

## 5. Possible Further Improvemnt

In the future, we will consider redesigning the enclosure of the physical device to ensure that it is aesthetically pleasing as well as easy to use, for example by adding a stand. In addition to this, we will also redesign the map, for example by adding more cities and redesigning the typography. In addition to this, the beautification of the mobile app will also take place.

Thank you for reading and using!