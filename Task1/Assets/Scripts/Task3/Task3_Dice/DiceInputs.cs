using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class DiceInputs : MonoBehaviour
{
    public UnityEvent dropEvent = new();
    public UnityEvent rotateEvent = new();

    private bool drop;
    private bool rotate;

    public void OnDrop(InputValue value)
    {
        drop = value.isPressed;
        dropEvent?.Invoke();
    }

    public void OnRotate(InputValue value)
    {
        rotate = value.isPressed;
        rotateEvent?.Invoke();
    }
}
