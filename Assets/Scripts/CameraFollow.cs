using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;   // Reference to the player object
    private Vector3 offset;     // Offset between camera and player

    public float smoothSpeed = 0.125f; // Smooth movement speed
    public float dragSpeed = 2f;       // Speed of camera dragging

    public Transform mapBounds;  // Reference to the map boundaries (Assign in Inspector)

    private bool isRecording = false; // Flag for recording mode
    private Vector3 dragOrigin;

    private float minX, maxX, minY, maxY;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (player != null)
        {
            offset = transform.position - player.transform.position;
        }

        CalculateCameraBounds();
    }

    void LateUpdate()
    {
        if (isRecording)
        {
            FollowPlayer();
        }
        else
        {
            HandleCameraDrag();
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.transform.position + offset;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void HandleCameraDrag()
    {
        if (Input.GetMouseButtonDown(2)) // Middle mouse button pressed
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2)) // Holding middle mouse button
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + difference * dragSpeed;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;
        }
    }

    public void StartRecording()
    {
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    private void CalculateCameraBounds()
    {
        if (mapBounds == null) return;

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculate bounds based on map size
        minX = mapBounds.position.x - (mapBounds.localScale.x / 2) + horzExtent;
        maxX = mapBounds.position.x + (mapBounds.localScale.x / 2) - horzExtent;

        minY = mapBounds.position.y - (mapBounds.localScale.y / 2) + vertExtent;
        maxY = mapBounds.position.y + (mapBounds.localScale.y / 2) - vertExtent;
    }
}
