using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSongButton : MonoBehaviour
{
    public Button thisButton;
    public Button otherButton1;
    public Button otherButton2;


    public void SelectThisButton()
    {
        thisButton.gameObject.transform.localScale = new Vector3(1.8f, 1.8f);
        otherButton1.gameObject.transform.localScale = Vector3.one;
        otherButton2.gameObject.transform.localScale = Vector3.one;
    }

}

