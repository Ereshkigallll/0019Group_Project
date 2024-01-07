using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// Data model class for the air quality response
[System.Serializable]
public class AirQualityResponse
{
    public string status;
    public AirQualityData data;
}

// Data model class for the air quality data
[System.Serializable]
public class AirQualityData
{
    public int aqi;
    public int idx;
    public Attribution[] attributions;
    public City city;
    public string dominentpol;
    public IAQI iaqi;
    public AirQualityTime time;
    public Forecast forecast;
    public DebugInfo debug;
}

// Class representing attribution information
[System.Serializable]
public class Attribution
{
    public string url;
    public string name;
    public string logo;
}

// Class representing city information
[System.Serializable]
public class City
{
    public float[] geo;
    public string name;
    public string url;
    public string location;
}

// Class representing individual air quality indices
[System.Serializable]
public class IAQI
{
    public AQIValue co;
    public AQIValue h;
    public AQIValue no2;
    public AQIValue o3;
    public AQIValue p;
    public AQIValue pm10;
    public AQIValue pm25;
    public AQIValue so2;
    public AQIValue t;
    public AQIValue w;
}

// Class for a single air quality index value
[System.Serializable]
public class AQIValue
{
    public float v;
}

// Class representing air quality time information
[System.Serializable]
public class AirQualityTime
{
    public string s;
    public string tz;
    public long v;
    public string iso;
}

// Class representing forecast information
[System.Serializable]
public class Forecast
{
    public DailyForecast daily;
}

// Class for daily forecast data
[System.Serializable]
public class DailyForecast
{
    public ForecastItem[] o3;
    public ForecastItem[] pm10;
    public ForecastItem[] pm25;
    public ForecastItem[] uvi;
}

// Class for individual forecast items
[System.Serializable]
public class ForecastItem
{
    public int avg;
    public string day;
    public int max;
    public int min;
}

// Class for debug information
[System.Serializable]
public class DebugInfo
{
    public string sync;
}

// API client for fetching air quality data
public class AirQualityAPIClient : MonoBehaviour
{
    private static readonly string baseURL = "https://api.waqi.info/feed/";
    private static readonly string token = "Your token here"; // Replace with your actual token

    // Coroutine to get weather data for a specific city
    public static IEnumerator GetWeatherDataForCity(string city, Action<AirQualityResponse> callback)
    {
        string url = $"{baseURL}{city}/?token={token}";
        Debug.Log($"[AirQualityAPIClient] Requesting data for {city}.");
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            // Check for network or HTTP errors
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                try
                {
                    // Parse the response and invoke the callback with the parsed data
                    string jsonString = request.downloadHandler.text;
                    Debug.Log($"[AirQualityAPIClient] Response from {city}: {jsonString}");
                    AirQualityResponse response = JsonUtility.FromJson<AirQualityResponse>(jsonString);
                    callback?.Invoke(response);
                }
                catch (Exception e)
                {
                    // Handle any exceptions during parsing
                    Debug.LogError("Exception parsing response: " + e.Message);
                }
            }
        }
    }
}
