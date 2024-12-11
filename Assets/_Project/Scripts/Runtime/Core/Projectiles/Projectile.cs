using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 _direction = Vector3.up;

    private float _speed = 0.0f;
    private int _damage = 1;
    private Camera _mainCamera;

    public EntityType EntityType { get; private set; }
    public event Action<Projectile> OnDestroy;

    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Initialize(EntityType type)
    {
        EntityType = type;
    }

    void Update()
    {
        MoveProjectile();
        CheckBounds();
    }

    private void MoveProjectile()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private void CheckBounds()
    {
        if (!IsVisibleInCamera())
        {
            DestroyProjectile();
        }
    }

    private bool IsVisibleInCamera()
    {
        if (_mainCamera == null) return true;

        Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(transform.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
               viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
               viewportPosition.z > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TryDamageTarget(other))
        {
            DestroyProjectile();
        }
    }

    private bool TryDamageTarget(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null) return false;
        damageable.TakeDamage(_damage);
        return true;
    }

    private void DestroyProjectile()
    {
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
        _trailRenderer?.Clear();

        OnDestroy?.Invoke(this);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}