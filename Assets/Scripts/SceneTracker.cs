using UnityEngine;

public class SceneTracker : MonoBehaviour
{
    public static string lastLevelScene;  // Lưu tên Level trước khi thua

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  // Giữ lại khi đổi scene
    }

    // Gọi hàm này khi chuẩn bị chuyển sang Lose scene
    public static void SetLastLevel(string sceneName)
    {
        lastLevelScene = sceneName;
    }
}
