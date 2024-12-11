using UnityEngine;

public class PowerUp : MonoBehaviour {
    private float _speed = 3.0f;

    private BoundsHandler _boundsHandler;

    public enum PowerUpType {
        FIRE_RATE = 0,
        HEALTH = 1,
        DAMAGE = 2,
        PROJECTILE_SPEED = 3
    }

    [SerializeField] private PowerUpType _type;

    private void Awake()
    {
        InitializeBoundsHandler();
    }

    public void SetType(PowerUpType type) {
        _type = type;
    }

    private void Update() {
        _boundsHandler.CheckBounds();
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);
    }

    private void InitializeBoundsHandler()
    {
        _boundsHandler = new BoundsHandler(transform);
        _boundsHandler.OnOutsideBounds += HandleOutsideBounds;
    }

    private void HandleOutsideBounds()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        
        var receiver = other.GetComponent<IPowerUpReceiver>();
        if (receiver == null) return;

        receiver.AddPowerUp(_type);
        Destroy(gameObject);
    }
}
