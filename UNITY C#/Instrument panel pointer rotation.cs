using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugePointer : MonoBehaviour
{
    public Transform pointerTransform; // Transform component of the pointer

    // Assumed maximum values for each pollutant indicator
    private float maxValuePM25 = 300;
    private float maxValuePM10 = 300;
    private float maxValueNO2 = 300;

    public void SetPointerValue(float value, string type)
    {
        float angle = 0f;
        // Calculate the rotation angle based on the type and value
        switch (type)
        {
            case "PM2.5":
                angle = Mathf.Lerp(0, 42, value / maxValuePM25); // Interpolate between 0 and 42 degrees for PM2.5
                break;
            case "PM10":
                angle = Mathf.Lerp(42, 84, value / maxValuePM10); // Interpolate between 42 and 84 degrees for PM10
                break;
            case "NO2":
                angle = Mathf.Lerp(84, 126, value / maxValueNO2); // Interpolate between 84 and 126 degrees for NO2
                break;
        }
        // Apply the rotation angle to the pointer
        pointerTransform.localEulerAngles = new Vector3(0, 0, -angle);
    }
}
