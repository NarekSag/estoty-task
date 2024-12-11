using UnityEngine;

public class EnemyMovement
{
    private Rigidbody _body;
    private float _speed;

    public EnemyMovement(Rigidbody body, float speed)
    {
        _body = body;
        _speed = speed;
    }

    public void Move()
    {
        Vector3 newPosition = _body.position + Vector3.down * (_speed * Time.deltaTime);
        _body.MovePosition(newPosition);
    }
}