using UnityEngine;

public class DanceArrow : MonoBehaviour
{
    public string direction;
    [SerializeField] float speed = 1f;  
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }
    private void Update()
    {
        targetPosition.y -= speed * Time.deltaTime;  
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }

    public void SetDirection(string direction)
    {
        this.direction = direction;
    }
}
