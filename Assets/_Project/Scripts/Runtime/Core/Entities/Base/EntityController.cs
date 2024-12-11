using System;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    [SerializeField] protected GameObject model;
    [SerializeField] protected ParticleSystem explosionParticle;

    protected Rigidbody Body;
    protected Collider Collider;

    private EntityExplosion _explosion;

    public EntityHealth Health { get; private set; }

    protected virtual void Awake()
    {
        Body = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Health = GetComponent<EntityHealth>();

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
        Health.Initialize(health);
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
        Collider.enabled = isActive;
    }

    protected abstract void FixedUpdate();
}