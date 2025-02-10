using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;     // Offset between camera and player

    public float smoothSpeed = 5f; // Smooth movement speed
    public float minX = -10f, maxX = 50f; // X-axis limits
    public float minY = -10f, maxY = 10f; // Y-axis limits

    public float dragSpeed = 2f; // Speed of camera dragging

    private bool isRecording = false; // Flag for recording mode
    private bool isDragging = false;  // Flag for camera dragging
    private Vector3 dragOrigin;       // Mouse drag start position

    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.transform.position;
        }
    }

    void LateUpdate()
    {
        if (isRecording || !isDragging)
        {
            FollowPlayer();  // Luôn follow player nếu đang record hoặc không kéo camera
        }

        HandleCameraDrag();
    }

    // Camera theo dõi player
    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.transform.position + offset;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    // Xử lý kéo camera
    private void HandleCameraDrag()
    {
        if (Input.GetMouseButtonDown(2)) // Nhấn chuột giữa
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;  // Bắt đầu kéo camera
        }

        if (Input.GetMouseButton(2)) // Giữ chuột giữa
        {
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 difference = Camera.main.ScreenToWorldPoint(dragOrigin) - Camera.main.ScreenToWorldPoint(currentMousePos);

            difference.z = 0f;

            Vector3 newPosition = transform.position + difference * dragSpeed;

            // Giới hạn camera trong phạm vi
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;

            dragOrigin = currentMousePos;
        }

        if (Input.GetMouseButtonUp(2)) // Nhả chuột giữa
        {
            isDragging = false;  // Kết thúc kéo camera
        }
    }

    // Bắt đầu quay, camera focus vào player
    public void StartRecording()
    {
        isRecording = true;
    }

    // Dừng quay, camera vẫn focus vào player
    public void StopRecording()
    {
        isRecording = false;
    }
}
