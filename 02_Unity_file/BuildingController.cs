using UnityEngine;

public class BuildingController : MonoBehaviour
{
    // Public array to add ball objects in the Inspector
    public GameObject[] balls;

    // Variable to track whether the balls are currently visible
    private bool areBallsVisible = false;

    void Start()
    {
        // Hide all balls on initialization
        ToggleBallsVisibility(false);
    }

    void Update()
    {
        // Check for touch input on the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Cast a ray from the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Check if the ray hits this game object
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                // Toggle the visibility state of the balls
                areBallsVisible = !areBallsVisible;
                ToggleBallsVisibility(areBallsVisible);
            }
        }
    }

    // Function to toggle the visibility of the balls
    private void ToggleBallsVisibility(bool isVisible)
    {
        foreach (var ball in balls)
        {
            if (ball != null)
            {
                // Set the ball's active state to the isVisible parameter
                ball.SetActive(isVisible);
            }
        }
    }
}
