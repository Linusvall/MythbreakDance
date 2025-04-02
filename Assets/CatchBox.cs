using UnityEngine;

public class CatchBox : MonoBehaviour
{
    [SerializeField] public string direction;
    [SerializeField] private PlayMode playMode;
    public bool isActive;
    private GameObject danceArrow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DanceArrow"))
        {
            if (other.GetComponent<DanceArrow>() != null)
            {
                danceArrow = other.gameObject;
                isActive = true;
                Invoke("DisableObject", 0.5f);
            }
        }
    }

    private void DisableObject()
    {
        Destroy(danceArrow);
        isActive = false;
    }
}
