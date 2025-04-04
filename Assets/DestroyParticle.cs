using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("DestroyParticleSystem", 1f);
    }

    private void DestroyParticleSystem()
    {
        Destroy(gameObject);
    }
}
