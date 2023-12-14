using UnityEngine;
using TMPro; // Ensure to include this for using TextMeshPro

public class WeatherDisplay : MonoBehaviour
{
    public TextMeshProUGUI weatherTextUI; // UI Text component to display weather data
    public string cityName; // Name of the city to display weather information
    public float updateInterval = 5.0f; // Time interval (in seconds) for updating weather data
    private float timer; // Timer to track update intervals

    // Boolean flags to control what data to display
    public bool showAQI = true; // Whether to display the overall Air Quality Index
    public bool showPM25 = true; // Whether to display PM2.5 data
    public bool showPM10 = true; // Whether to display PM10 data
    public bool showNO2 = true; // Whether to display NO2 data

    void Update()
    {
        // Update the timer each frame
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            // Update weather data when the timer exceeds the interval
            UpdateWeatherData();
            timer = 0; // Reset timer
        }
    }

    void UpdateWeatherData()
    {
        // Get weather data for the specified city
        AirQualityResponse weatherData = WeatherService.Instance.GetDataForCity(cityName);
        if (weatherData != null)
        {
            // Construct the string to display based on selected data
            string weatherInfo = "";
            if (showAQI)
            {
                weatherInfo += $"AQI: {weatherData.data.aqi}"; // 替换“(单位)”为AQI的单位
            }
            if (showPM25)
            {
                weatherInfo += $" PM2.5: {weatherData.data.iaqi.pm25.v} µg/m³"; // 微克每立方米
            }
            if (showPM10)
            {
                weatherInfo += $" PM10: {weatherData.data.iaqi.pm10.v} µg/m³"; // 微克每立方米
            }
            if (showNO2)
            {
                weatherInfo += $" NO2: {weatherData.data.iaqi.no2.v} ppm"; // 部分每百万
            }

            // Update the UI text with the constructed information
            weatherTextUI.text = weatherInfo;
        }
    }
}


