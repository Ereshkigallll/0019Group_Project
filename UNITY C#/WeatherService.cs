using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherService : MonoBehaviour
{
    public static WeatherService Instance { get; private set; }

    private WeatherDataBuffer dataBuffer = new WeatherDataBuffer();
    // List of cities for which to fetch weather data
    private string[] cities = { "london", "paris", "rome", "berlin", "oslo", "stockholm", "warsaw", "barcelona" };

    void Awake()
    {
        // Singleton pattern to ensure only one instance of WeatherService exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateWeatherData(); 
        // Start the coroutine to update weather data
        StartCoroutine(UpdateWeatherDataRoutine());
    }

    IEnumerator UpdateWeatherDataRoutine()
    {
        // Continuously update weather data every 60 seconds
        while (true)
        {
            yield return new WaitForSeconds(60);
            UpdateWeatherData();
        }
    }

    void UpdateWeatherData()
    {
        // Request weather data for each city
        foreach (var city in cities)
        {
            StartCoroutine(AirQualityAPIClient.GetWeatherDataForCity(city, (response) => OnDataReceived(city, response)));
        }
    }

    void OnDataReceived(string city, AirQualityResponse response)
    {
        // Handle the received data for each city
        if (response != null && response.status == "ok")
        {
            dataBuffer.UpdateData(city, response);
        }
        else
        {
            Debug.LogError("Received invalid data for " + city);
        }
    }

    public AirQualityResponse GetDataForCity(string city)
    {
        // Retrieve weather data for a specific city
        return dataBuffer.GetDataForCity(city);
    }

    // Debug method to print all weather data to the console
     public void PrintAllWeatherData()
    {
        foreach (var cityData in dataBuffer.GetAllData())
        {
            var cityName = cityData.Key;
            var weatherData = cityData.Value.data;

            // Retrieve the overall AQI, PM2.5, and NO2 values
            var aqiValue = weatherData.aqi;
            float pm25Value = weatherData.iaqi?.pm25?.v ?? float.NaN; // Safe check for PM2.5
            float no2Value = weatherData.iaqi?.no2?.v ?? float.NaN;   // Safe check for NO2
            float pm10Value = weatherData.iaqi?.pm10?.v ?? float.NaN;
            // Print the information to the console
            Debug.Log($"City: {cityName}, AQI: {aqiValue}, PM2.5: {pm25Value},PM10: {pm10Value}, NO2: {no2Value}");
        }
    }
}

public class WeatherDataBuffer
{
    // Dictionary to hold the latest weather data for each city
    private Dictionary<string, AirQualityResponse> latestData = new Dictionary<string, AirQualityResponse>();

    public void UpdateData(string city, AirQualityResponse newData)
    {
        // Update the weather data for a specific city
        latestData[city] = newData;
    }

    public AirQualityResponse GetDataForCity(string city)
    {
        // Try to get the weather data for a specific city
        if (latestData.TryGetValue(city, out var data))
        {
            return data;
        }
        return null; // Return null if the city data is not found
    }

    // Method to get all the weather data stored in the buffer
    public Dictionary<string, AirQualityResponse> GetAllData()
    {
        return latestData;
    }
}

// Include all your data model classes here (AirQualityResponse, AirQualityData, Attribution, etc.)


