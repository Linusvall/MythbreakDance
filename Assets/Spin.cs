using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 30f; // Degrees per second, can be adjusted in Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
