using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    private void Awake()
    {

        // Ensure the PlayerInputManager is assigned
        if (playerInputManager == null)
        {
            playerInputManager = FindObjectOfType<PlayerInputManager>();
        }
    }

    // This function will be triggered when the join action is triggered
    public void OnJoinGame(InputAction.CallbackContext context)
    {
        if (context.started) // When the button is pressed down
        {
            // This will add a new player when the button is pressed
            playerInputManager.JoinPlayer();
        }
    }
}
