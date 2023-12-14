using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引用TextMeshPro命名空间

public class LedDisplay : MonoBehaviour
{
    public TextMeshProUGUI cityText; // 用于显示城市名称的Text Mesh Pro UI组件
    public TextMeshProUGUI aqiText; // 用于显示AQI数值的Text Mesh Pro UI组件

    // 设置并更新LED显示屏上的文本信息
    public void SetDisplay(string city, int aqi)
    {
        cityText.text = city;
        aqiText.text = $"AQI: {aqi}";
    }
}

