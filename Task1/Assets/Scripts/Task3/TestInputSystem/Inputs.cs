using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Inputs : MonoBehaviour
{

    public UnityEvent shootEvent = new();
    public UnityEvent jumpEvent = new();

    public Vector2 move;
    public Vector2 look;
    public bool shoot;
    public bool jump;

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    public void OnShoot(InputValue value)
    {
        shoot = value.isPressed;
        shootEvent?.Invoke();
    }

    public void OnJump(InputValue value)
    {
        jump = value.isPressed;
        jumpEvent?.Invoke();
    }
}
