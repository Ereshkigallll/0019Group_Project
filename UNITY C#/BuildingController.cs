using UnityEngine;

public class BuildingController : MonoBehaviour
{
    // Public array for adding sphere objects in the Inspector
    public GameObject[] balls;

    // Tracks whether spheres are currently visible
    private bool areBallsVisible = false;

    void Start()
    {
        // Hide all spheres on initialization
        ToggleBallsVisibility(false);
    }

    void Update()
    {
        // Detect screen touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Cast a ray from the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Check if the ray hits this game object
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                // Toggle the visibility state of the spheres
                areBallsVisible = !areBallsVisible;
                ToggleBallsVisibility(areBallsVisible);
            }
        }
    }

    // Toggle the visibility of the spheres
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
