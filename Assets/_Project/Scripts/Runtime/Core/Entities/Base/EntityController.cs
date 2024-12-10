using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    [SerializeField] protected GameObject model;
    [SerializeField] protected ParticleSystem explosionParticle;

    protected Rigidbody _body;
    private EntityHealth _health;

    public EntityHealth Health => _health;

    protected virtual void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public virtual void Initialize(float health)
    {
        InitializeHealth(health);
        
        ToggleExplosionParticle(false);
        model.SetActive(true);
    }

    private void InitializeHealth(float health)
    {
        _health = new EntityHealth(health);
        _health.OnDeath += HandleDeath;
    }

    protected virtual void HandleDeath()
    {
        ToggleExplosionParticle(true);
        model.SetActive(false);
    }

    private void ToggleExplosionParticle(bool state)
    {
        if (state)
        {
            explosionParticle.gameObject.SetActive(true);
            explosionParticle.Play();
        }
        else
        {
            explosionParticle.gameObject.SetActive(false);
            explosionParticle.Stop();
        }
    }

    protected abstract void FixedUpdate();
}