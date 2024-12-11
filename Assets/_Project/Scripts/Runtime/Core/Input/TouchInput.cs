using UnityEngine;
using UnityInput = UnityEngine.Input;

public class TouchInput : IInput
{
    public Vector3 Direction => GetDirection();

    private Vector3 GetDirection() => UnityInput.GetTouch(0).position;
}