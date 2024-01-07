using UnityEngine;
using System.Collections;
using NativeWebSocket;

public class WebSocketClient : MonoBehaviour
{
    WebSocket websocket;

    async void Start()
    {
        // 创建WebSocket实例并连接到ESP8266的WebSocket服务器
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

    // 调用此方法来发送按钮上的文字到ESP8266
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
