using UnityEngine;

public class BuildingController : MonoBehaviour
{
    // 公共数组，用于在Inspector中添加小球对象
    public GameObject[] balls;

    // 用于跟踪小球是否正在显示
    private bool areBallsVisible = false;

    void Start()
    {
        // 初始化时隐藏所有小球
        ToggleBallsVisibility(false);
    }

    void Update()
    {
        // 检测屏幕触摸输入
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // 从触摸位置发射一条射线
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // 检测射线是否击中了此游戏对象
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                // 切换小球的显示状态
                areBallsVisible = !areBallsVisible;
                ToggleBallsVisibility(areBallsVisible);
            }
        }
    }

    // 切换小球的显示和隐藏
    private void ToggleBallsVisibility(bool isVisible)
    {
        foreach (var ball in balls)
        {
            if (ball != null)
            {
                ball.SetActive(isVisible);
            }
        }
    }
}
