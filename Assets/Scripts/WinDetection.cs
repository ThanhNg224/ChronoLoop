using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetection : MonoBehaviour
{
    public string winSceneName = "Win"; // Name of the scene to load when the player wins

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the win object
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with win object. Loading win scene...");
            LoadWinScene();
        }
    }

    private void LoadWinScene()
    {
        // Load the win scene
        SceneManager.LoadScene(winSceneName);
    }
}
