using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DanceArrow : MonoBehaviour
{
    public string direction;
    private float t;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    [SerializeField] private float timeToReachTarget;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {

        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);

    }

    public void SetDirection(string direction)
    {
        this.direction = direction;
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
       
    }
}
