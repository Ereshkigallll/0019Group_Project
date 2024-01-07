using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIdisappear : MonoBehaviour
{
    public GameObject uiElement; // UI元素的引用

    void Start()
    {
        // 开始计时器协程
        StartCoroutine(HideUIAfterTime(5)); // 10秒后隐藏UI
    }

    IEnumerator HideUIAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // 等待指定的时间
        uiElement.SetActive(false); // 隐藏UI元素
    }
}
