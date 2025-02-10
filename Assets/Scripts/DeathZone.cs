using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the DeathZone
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered DeathZone. Reloading scene...");
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        // Reload the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
