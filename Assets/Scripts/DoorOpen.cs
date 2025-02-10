using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private GameObject door; // Reference to the door

    public float doorOpenHeight = 4.2f;  // How high the door moves when opened
    public float openSpeed = 2f;       // Speed of door opening

    private bool doorOpened = false;   // Check if the door has been opened

    void Start()
    {
        // Find the door object by tag
        door = GameObject.FindGameObjectWithTag("Door");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with the DoorOpener
        if (other.CompareTag("DoorOpener") && !doorOpened)
        {
            doorOpened = true;  // Prevent repeated opening
            StartCoroutine(OpenDoor());
        }
    }

    // Coroutine to smoothly move the door upward
    private System.Collections.IEnumerator OpenDoor()
    {
        Vector3 targetPosition = door.transform.position + new Vector3(0, doorOpenHeight, 0);

        // Move the door upwards smoothly
        while (Vector3.Distance(door.transform.position, targetPosition) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(
                door.transform.position,
                targetPosition,
                openSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}
