using UnityEngine;

public class CatchBox : MonoBehaviour
{
    [SerializeField] public string direction;
    [SerializeField] private PlayMode playMode;
    [SerializeField] private DirectionManager directionManager;
    private GameObject danceArrow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DanceArrow"))
        {
            if (other.GetComponent<DanceArrow>() != null)
            {
                danceArrow = other.gameObject;
                directionManager.ActivateSubCatchBox(direction);
                Invoke("DisableObject", 0.5f);
            }
        }
    }

    private void DisableObject()
    {
        Destroy(danceArrow);
    }
}
