using UnityEngine;
using UnityEngine.UI;

public class DanceTitleTrack : MonoBehaviour
{
    public Text text;
    public float minIntensity = 0.5f;
    public float maxIntensity = 3f;
    public float flickerSpeed = 0.1f;

    private Material textMaterial;
    private float timer;

    void Start()
    {
        textMaterial = text.material;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= flickerSpeed)
        {
            float intensity = Random.Range(minIntensity, maxIntensity);
            textMaterial.SetColor("_EmissionColor", Color.white * intensity);
            timer = 0;
        }
    }
}
