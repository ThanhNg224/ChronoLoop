using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloadTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Press 'R' to reload
        {
            Debug.Log("Reloading scene for testing...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
