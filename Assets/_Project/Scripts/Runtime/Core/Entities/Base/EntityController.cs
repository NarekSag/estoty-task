using System;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    [SerializeField] protected GameObject model;
    [SerializeField] protected ParticleSystem explosionParticle;

    protected Rigidbody _body;
    protected Collider _collider;

    private EntityHealth _health;
    private EntityExplosion _explosion;

    public EntityHealth Health => _health;

    public bool IsDead { get; private set; }

    protected virtual void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _explosion = new EntityExplosion(explosionParticle);
    }

    public virtual void Initialize(float health)
    {
        InitializeHealth(health);

        _explosion.Stop();
        ToggleEntityState(true);
    }

    private void InitializeHealth(float health)
    {
        _health = new EntityHealth(health);
        Health.OnDeath += HandleDeath;
    }

    protected virtual void HandleDeath()
    {
        _explosion.Play();
        ToggleEntityState(false);
    }

    protected void ToggleEntityState(bool isActive)
    {
        model.SetActive(isActive);
        _collider.enabled = isActive;
        IsDead = !isActive;
    }

    protected abstract void FixedUpdate();
}