using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// 数据模型类
[System.Serializable]
public class AirQualityResponse
{
    public string status;
    public AirQualityData data;
}

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

[System.Serializable]
public class Attribution
{
    public string url;
    public string name;
    public string logo;
}

[System.Serializable]
public class City
{
    public float[] geo;
    public string name;
    public string url;
    public string location;
}

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

[System.Serializable]
public class AQIValue
{
    public float v;
}

[System.Serializable]
public class AirQualityTime
{
    public string s;
    public string tz;
    public long v;
    public string iso;
}

[System.Serializable]
public class Forecast
{
    public DailyForecast daily;
}

[System.Serializable]
public class DailyForecast
{
    public ForecastItem[] o3;
    public ForecastItem[] pm10;
    public ForecastItem[] pm25;
    public ForecastItem[] uvi;
}

[System.Serializable]
public class ForecastItem
{
    public int avg;
    public string day;
    public int max;
    public int min;
}

[System.Serializable]
public class DebugInfo
{
    public string sync;
}

// API客户端
public class AirQualityAPIClient : MonoBehaviour
{
    private static  readonly string baseURL = "https://api.waqi.info/feed/";
    private static readonly string token = "3f944813adceac55004626fbea5143afdd666e42"; 

    public static IEnumerator GetWeatherDataForCity(string city, Action<AirQualityResponse> callback)
    {
        string url = $"{baseURL}{city}/?token={token}";
 Debug.Log($"[AirQualityAPIClient] Requesting data for {city}.");
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                try
                {
                    string jsonString = request.downloadHandler.text;
                    Debug.Log($"[AirQualityAPIClient] Response from {city}: {jsonString}");
                    AirQualityResponse response = JsonUtility.FromJson<AirQualityResponse>(jsonString);
                    callback?.Invoke(response);
                }
                catch (Exception e)
                {
                    Debug.LogError("Exception parsing response: " + e.Message);
                }
            }
        }
    }
}
