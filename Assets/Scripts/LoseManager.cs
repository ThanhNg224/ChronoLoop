using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    public void PlayAgain()
    {
        if (!string.IsNullOrEmpty(SceneTracker.lastLevelScene))
        {
            SceneManager.LoadScene(SceneTracker.lastLevelScene);
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
