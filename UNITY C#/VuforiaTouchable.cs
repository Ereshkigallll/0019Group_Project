using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class VuforiaTouchable : MonoBehaviour
{
    public string hideableTag = "HideableModel"; // Tag for objects that can be hidden
    private bool isObjectsHidden = false; // Tracks the visibility state of objects
    private List<GameObject> hiddenObjects = new List<GameObject>(); // List to store hidden objects

    private WebSocket webSocket; // WebSocket variable
    public string webSocketServerUrl = "ws://10.129.125.175:81"; // URL of the WebSocket server
    public string cityName; // City name associated with the building

    async void Start()
    {
        // Initialize WebSocket connection
        webSocket = new WebSocket(webSocketServerUrl);
        await webSocket.Connect();
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // Raycast to check if the building model is touched
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                // Toggle object visibility
                if (isObjectsHidden)
                {
                    ShowObjects();
                }
                else
                {
                    HideObjects();
                    SendCityNameToESP8266(); // Call method to send city name
                }
                isObjectsHidden = !isObjectsHidden;
            }
        }
    }

    // Method to hide objects
    void HideObjects()
    {
        var allObjects = GameObject.FindGameObjectsWithTag(hideableTag);
        foreach (var obj in allObjects)
        {
            if (obj != gameObject)
            {
                hiddenObjects.Add(obj);
                obj.SetActive(false);
            }
        }
    }

    // Method to show objects
    void ShowObjects()
    {
        foreach (var obj in hiddenObjects)
        {
            obj.SetActive(true);
        }
        hiddenObjects.Clear();
    }

    // Method to send city name to ESP8266
    async void SendCityNameToESP8266()
    {
        Debug.Log("Attempting to send city name to ESP8266");

        if (webSocket != null && webSocket.State == WebSocketState.Open)
        {
            try
            {
                await webSocket.SendText(cityName);
                Debug.Log("Sent city name to ESP8266: " + cityName);
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to send city name: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("WebSocket is not open or is null");
        }
    }

    // Cleanup on destruction
    private async void OnDestroy()
    {
        if (webSocket != null)
        {
            await webSocket.Close();
        }
    }
}
