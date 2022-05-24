using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool type;

    [Header("Interaction")]
    public bool interact;

    [Header("UI")]
    public bool upgradeMenu;
    public bool taskMenu;

    [Header("Toggles")]
    public bool allowLook;
    public bool allowMove;

    public void OnMove(InputAction.CallbackContext value)
    {
        move = allowMove ? value.ReadValue<Vector2>() : Vector2.zero;
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        look = allowLook ? value.ReadValue<Vector2>() : Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        jump = allowMove && value.ReadValueAsButton();
        type = value.ReadValueAsButton();
    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        sprint = value.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        interact = value.ReadValueAsButton();
    }

    public void OnUpgradeMenu(InputAction.CallbackContext value)
    {
        upgradeMenu = value.ReadValueAsButton();
    }

    public void OnTaskMenu(InputAction.CallbackContext value)
    {
        taskMenu = value.ReadValueAsButton();
    }
}
