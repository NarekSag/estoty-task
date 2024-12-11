using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EntityCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable == null) return;
        
        damageable.Kill();
    }
}
