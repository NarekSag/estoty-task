using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    [SerializeField] protected GameObject _prefabExplosion;

    protected Rigidbody _body;
    protected EntityHealth _health;

    public EntityHealth Health => _health;

    protected virtual void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public virtual void Initialize(float health)
    {
        InitializeHealth(health);
    }

    protected void InitializeHealth(float health)
    {
        _health = new EntityHealth(health);
        _health.OnDeath += HandleDeath;
    }

    protected virtual void HandleDeath()
    {
        var fx = Instantiate(_prefabExplosion);
        fx.transform.position = transform.position;
        Destroy(gameObject);
    }

    protected abstract void FixedUpdate();
}