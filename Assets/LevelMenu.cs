using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void OpenLevel(int levelIndex)
    {
       string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    }
    
        
    
}
