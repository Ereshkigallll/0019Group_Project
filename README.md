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

#### 3.3.1 `AirQualityAPIClient.cs`
This document is primarily used to get air quality information from [this site](https://aqicn.org)'s API in Unity.

:warning: Please replace the API token within the file with your own token from the website before using it.

#### 3.3.2 `BuildingController.cs`
This script is mainly used to control the appearance and disappearance of small spheres and buttons on the side of the building model that are used to display air quality information. When no city is selected, the spheres and buttons are hidden by default. When the user clicks on a landmark, the spheres and buttons for that city will appear. At the same time, the values of the corresponding air quality components will appear on the spheres. 
