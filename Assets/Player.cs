using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] PlayerInput playerInput;
    public DirectionManager directionManager;

    private void Start()
    {
        GameObject myObject = GameObject.Find("CatchBox" + playerInput.playerIndex);
        directionManager = myObject.GetComponent<DirectionManager>();
    }
    public void ActivateDanceDirection(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        directionManager.SendDanceDirectionPlayer(context.ReadValue<Vector2>(), this);
       
    }
}
