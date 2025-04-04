using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    private string level;
   public void SetLevel(string levelName)
    {
        level = levelName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(level);
    }
    public void LoadMainMenu()
    {
        print("Wa");
        SceneManager.LoadScene("MainMenu");
    }
}
