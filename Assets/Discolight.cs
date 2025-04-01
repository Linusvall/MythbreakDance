using UnityEngine;

public class Discolight : MonoBehaviour
{
    private Light discoLight;
    [SerializeField] private float bpm;
    [SerializeField] private bool invertFlashing = false;
    private float lightIntensity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        discoLight = GetComponent<Light>();
        lightIntensity = discoLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        float secondsPerBeat = 60f / bpm;
        float elapsedTime = Time.time;

        int currentBeat = Mathf.FloorToInt(elapsedTime / secondsPerBeat) % 2;

        if (invertFlashing)
        {
            currentBeat = (currentBeat + 1) % 2;
        }

        if (currentBeat == 0)
        {
            discoLight.intensity = lightIntensity;
        }
        else
        {
            discoLight.intensity = 0;
        }
    }
}
