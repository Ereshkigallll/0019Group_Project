using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class VuforiaTouchable : MonoBehaviour
{
    public string hideableTag = "HideableModel"; // Tag used to identify hideable objects
    private bool isObjectsHidden = false; // Flag to track the visibility of objects
    private List<GameObject> hiddenObjects = new List<GameObject>(); // List to store hidden objects

    private WebSocket webSocket; // WebSocket instance
    public string webSocketServerUrl = "ws://10.129.125.175:81"; // WebSocket server address
    public string cityName; // Name of the city associated with the building

    async void Start()
    {
        // Initialize WebSocket connection
        webSocket = new WebSocket(webSocketServerUrl);
        await webSocket.Connect(); // Connect to the WebSocket server
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // Raycast to detect touch on the object
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                // Toggle visibility of objects
                if (isObjectsHidden)
                {
                    ShowObjects(); // Show hidden objects
                }
                else
                {
                    HideObjects(); // Hide objects
                    SendCityNameToESP8266(); // Call method to send city name to ESP8266
                }
                isObjectsHidden = !isObjectsHidden;
            }
        }
    }

    void HideObjects()
    {
        // Find and hide objects with the specified tag
        var allObjects = GameObject.FindGameObjectsWithTag(hideableTag);
        foreach (var obj in allObjects)
        {
            if (obj != gameObject)
            {
                hiddenObjects.Add(obj);
                obj.SetActive(false); // Hide object
            }
        }
    }

    void ShowObjects()
    {
        // Show all previously hidden objects
        foreach (var obj in hiddenObjects)
        {
            obj.SetActive(true); // Make object visible
        }
        hiddenObjects.Clear(); // Clear the list of hidden objects
    }

    async void SendCityNameToESP8266()
    {
        Debug.Log("Attempting to send city name to ESP8266");

        // Check if WebSocket is open before sending data
        if (webSocket != null && webSocket.State == WebSocketState.Open)
        {
            try
            {
                await webSocket.SendText(cityName); // Send city name over WebSocket
                Debug.Log("Sent city name to ESP8266: " + cityName);
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to send city name: " + ex.Message); // Log any errors
            }
        }
        else
        {
            Debug.LogError("WebSocket is not open or is null"); // Error if WebSocket is not open
        }
    }

    private async void OnDestroy()
    {
        // Close WebSocket connection when the object is destroyed
        if (webSocket != null)
        {
            await webSocket.Close();
        }
    }
}
