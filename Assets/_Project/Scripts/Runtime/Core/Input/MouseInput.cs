using UnityEngine;
using UnityInput = UnityEngine.Input;

public class MouseInput : IInput
{
    public Vector3 Direction => GetDirection();

    private Vector3 GetDirection() => UnityInput.mousePosition;
}
