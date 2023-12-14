using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIdisappear : MonoBehaviour
{
    public GameObject uiElement; // Reference to the UI element

    void Start()
    {
        // Start the timer coroutine
        StartCoroutine(HideUIAfterTime(5)); // Hide the UI after 5 seconds
    }

    IEnumerator HideUIAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // Wait for the specified time
        uiElement.SetActive(false); // Hide the UI element
    }
}
