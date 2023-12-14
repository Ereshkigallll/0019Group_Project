using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class VuforiaTouchable : MonoBehaviour
{
    public string hideableTag = "HideableModel";
    private bool isObjectsHidden = false;
    private List<GameObject> hiddenObjects = new List<GameObject>();

    private WebSocket webSocket;
    public string webSocketServerUrl = "ws://10.129.125.175:81"; // WebSocket服务器地址
    public string cityName; // 与建筑物关联的城市名称

    async void Start()
    {
        // 初始化WebSocket连接
        webSocket = new WebSocket(webSocketServerUrl);
        await webSocket.Connect();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                if (isObjectsHidden)
                {
                    ShowObjects();
                }
                else
                {
                    HideObjects();
                    SendCityNameToESP8266(); // 直接调用发送方法
                }
                isObjectsHidden = !isObjectsHidden;
            }
        }
    }

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

    void ShowObjects()
    {
        foreach (var obj in hiddenObjects)
        {
            obj.SetActive(true);
        }
        hiddenObjects.Clear();
    }

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

    private async void OnDestroy()
    {
        if (webSocket != null)
        {
            await webSocket.Close();
        }
    }
}
