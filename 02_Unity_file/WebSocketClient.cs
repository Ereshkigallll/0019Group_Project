using UnityEngine;
using System.Collections;
using NativeWebSocket;

public class WebSocketClient : MonoBehaviour
{
    WebSocket websocket;

    async void Start()
    {
        // Create a WebSocket instance and connect to the ESP8266 WebSocket server
        websocket = new WebSocket("your web socket server address");

        websocket.OnOpen += () =>
        {
            // Log a message when the connection is opened
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            // Log an error message if there's an issue with the WebSocket
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            // Log a message when the connection is closed
            Debug.Log("Connection closed!");
        };

        // Connect to the WebSocket server
        await websocket.Connect();
    }

    async void Update()
    {
        // Dispatch incoming messages (not applicable for WebGL builds except in the Unity Editor)
        #if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
        #endif
    }

    // Call this method to send the text of a button to the ESP8266
    public async void SendMessageToESP(string message)
    {
        // Send a text message if the WebSocket is open
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
        }
    }

    private async void OnApplicationQuit()
    {
        // Close the WebSocket when the application is quitting
        await websocket.Close();
    }
}
