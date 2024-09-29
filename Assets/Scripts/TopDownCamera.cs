using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float distanceFromPlayer = 10f; // Distance from the player
    public float rotationSpeed = 5f; // Speed of camera rotation
    public float scrollSpeed = 2f; // Speed of zooming in and out
    public float minDistance = 5f; // Minimum distance from the player
    public float maxDistance = 20f; // Maximum distance from the player
    public float heightAbovePlayer = 10f; // Fixed height above the player
    private float currentRotation; // Current rotation angle

    void Start()
    {
        // Set the initial rotation based on the camera's current angle
        currentRotation = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Get mouse input for rotating the camera only when the right mouse button is held down
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            currentRotation += horizontalInput * rotationSpeed; // Update the rotation angle
        }

        // Get mouse scroll input for zooming in and out
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distanceFromPlayer -= scrollInput * scrollSpeed; // Adjust zoom level based on scroll input and scroll speed
        distanceFromPlayer = Mathf.Clamp(distanceFromPlayer, minDistance, maxDistance); // Clamp zoom level

        // Calculate the new camera position based on fixed height and distance
        Vector3 offset = new Vector3(0, 0, -distanceFromPlayer); // Offset from player in Z direction
        Quaternion rotation = Quaternion.Euler(30f, currentRotation, 0); // Fixed pitch angle at 30 degrees

        // Calculate the new camera position
        Vector3 newPosition = player.position + rotation * offset; // Calculate the new position based on rotation

        // Set the camera's Y position above the player
        newPosition.y = player.position.y + heightAbovePlayer; 

        // Update camera position and rotation
        transform.position = newPosition; // Update camera position
        transform.rotation = rotation; // Maintain fixed pitch angle and rotate around Y-axis
        transform.LookAt(player.position); // Ensure the camera looks at the player
    }
}
