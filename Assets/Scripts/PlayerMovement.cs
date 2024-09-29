using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    public Transform cameraTransform; // Reference to the camera's transform
    private float angle; // Initialize angle variable

    void Update()
    {
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float verticalInput = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow

        // Calculate movement direction relative to the camera
        Vector3 forward = cameraTransform.forward; // Get the camera's forward vector
        Vector3 right = cameraTransform.right; // Get the camera's right vector

        forward.y = 0; // Ensure the forward vector is flat (ignore vertical component)
        right.y = 0; // Ensure the right vector is flat (ignore vertical component)

        forward.Normalize(); // Normalize the forward vector
        right.Normalize(); // Normalize the right vector

        // Calculate the movement direction
        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // Move the player
        if (moveDirection.magnitude >= 0.1f)
        {
            // Calculate the angle to rotate towards
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg; // Convert to degrees
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref angle, 0.1f); // Smoothly rotate towards the target angle
            transform.rotation = Quaternion.Euler(0, angle, 0); // Rotate the player

            // Move in the forward direction
            transform.position += moveDirection * moveSpeed * Time.deltaTime; // Move the player
        }
    }
}
