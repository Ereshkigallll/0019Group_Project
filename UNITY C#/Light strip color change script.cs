using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LightBar : MonoBehaviour
{
    public Renderer lightBarRenderer;

    public void SetAQILevel(int aqi)
    {
        Color color = GetColorForAQI(aqi);
        lightBarRenderer.material.color = color;
    }

    private Color GetColorForAQI(int aqi)
    {
       if (aqi <= 50) return new Color(67/255f, 151/255f, 106/255f);
    else if (aqi <= 100) return new Color(250/255f, 223/255f, 89/255f);
    else if (aqi <= 150) return new Color(241/255f, 158/255f, 74/255f);
    else if (aqi <= 200) return new Color(187/255f, 39/255f, 56/255f);
    else if (aqi <= 300) return new Color(93/255f, 14/255f, 147/255f);
    else return new Color(115/255f, 20/255f, 37/255f); // Default is the highest AQI level
    }
}

