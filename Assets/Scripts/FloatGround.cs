using UnityEngine;

public class FloatGround : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;        // Speed of left-right movement
    public float moveDistance = 3f;     // Distance to move from the starting point

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // Save the starting position
    }

    void Update()
    {
        // Move left and right using a sine wave
        float offsetX = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPosition.x + offsetX, startPosition.y, startPosition.z);
    }
}
