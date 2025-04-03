
using UnityEngine;
using UnityEngine.InputSystem;

public class EditMode : MonoBehaviour
{
    [SerializeField] private BeatmapGenerator beatmapGenerator;
    [SerializeField] InputAction editModeAction;

    private bool editing;
    private float editingStartTime;

  
    private void OnEnable()
    {
        editModeAction.Enable();
        editModeAction.performed += OnDPadPressed;
    }

    private void OnDisable()
    {
        editModeAction.Disable();
        editModeAction.performed -= OnDPadPressed;  
    }


    private void OnDPadPressed(InputAction.CallbackContext context)
    {
        if (!editing)
        {
            return;
        }

        Vector2 dpadValue = editModeAction.ReadValue<Vector2>();

        switch (true)
        {
            case bool _ when dpadValue.x < 0:
                Debug.Log("VÄNSTER");
                beatmapGenerator.AddToBeatMap(GetCurrentTime(), "Left");
                break;

            case bool _ when dpadValue.x > 0:
                Debug.Log("HÖGER");
                beatmapGenerator.AddToBeatMap(GetCurrentTime(), "Right");
                break;

            case bool _ when dpadValue.y > 0:
                Debug.Log("UPPÅT");
                beatmapGenerator.AddToBeatMap(GetCurrentTime(), "Up");
                break;

            case bool _ when dpadValue.y < 0:
                Debug.Log("NEDÅT");
                beatmapGenerator.AddToBeatMap(GetCurrentTime(), "Down");
                break;
        }
    }

    public void StartEditing(string songName)
    {
        beatmapGenerator.SetCurrentBeatMapName(songName);

        editing = true;
        editingStartTime = Time.realtimeSinceStartup;
    }
    private float GetCurrentTime()
    {
        return Time.realtimeSinceStartup - editingStartTime;
    }
    

}
