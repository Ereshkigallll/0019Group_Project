using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugePointer : MonoBehaviour
{
    public Transform pointerTransform; // 指针的Transform组件

    // 假设每种污染物指标的最大值
    private float maxValuePM25 = 300;
    private float maxValuePM10 = 300;
    private float maxValueNO2 = 300;

    public void SetPointerValue(float value, string type)
    {
        float angle = 0f;
        // 根据类型和数值计算出旋转角度
        switch (type)
        {
            case "PM2.5":
                angle = Mathf.Lerp(0, 42, value / maxValuePM25); 
                break;
            case "PM10":
                angle = Mathf.Lerp(42, 84, value / maxValuePM10); 
                break;
            case "NO2":
                angle = Mathf.Lerp(84, 126, value / maxValueNO2); 
                break;
        }
        // 应用旋转角度到指针
        pointerTransform.localEulerAngles = new Vector3(0, 0, -angle);
    }
}
