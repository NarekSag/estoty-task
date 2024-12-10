using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private const float MovementRangeMin = -3;
    private const float MovementRangeMax = 3;

    private IInput _input;
    private Rigidbody _body;

    public PlayerInput(IInput input, Rigidbody body)
    {
        _input = input;
        _body = body;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            Move();
    }

    private void Move()
    {
        float xPos = _input.Direction.x / Screen.width;
        _body.MovePosition(new Vector3(Mathf.Lerp(MovementRangeMin, MovementRangeMax, xPos), 0.0f, 0.0f));
    }

}
