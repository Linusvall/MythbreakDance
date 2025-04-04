using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

