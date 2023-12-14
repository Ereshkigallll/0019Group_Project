using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Reference the TextMeshPro namespace

public class LedDisplay : MonoBehaviour
{
    public TextMeshProUGUI cityText; // Text Mesh Pro UI component for displaying city names
    public TextMeshProUGUI aqiText; // Text Mesh Pro UI component for displaying AQI values

    // Set and update text information on the LED display
    public void SetDisplay(string city, int aqi)
    {
        cityText.text = city;
        aqiText.text = $"AQI: {aqi}";
    }
}

