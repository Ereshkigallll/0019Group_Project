using UnityEngine;
using System.Collections;
using NativeWebSocket;

public class WebSocketClient : MonoBehaviour
{
    WebSocket websocket;

    async void Start()
    {
        //Create a WebSocket instance and connect to the ESP8266's WebSocket server
        websocket = new WebSocket("ws://192.168.1.112:81");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        await websocket.Connect();
    }

    async void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
        #endif
    }

    // Call this method to send the text on the button to ESP8266
    public async void SendMessageToESP(string message)
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
