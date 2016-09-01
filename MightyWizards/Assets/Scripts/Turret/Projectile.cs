using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour {

    private float damage;
    private bool destroyOnImpact;

    public void SetDestroyOnImpact(bool destroyOnImpact)
    {
        this.destroyOnImpact = destroyOnImpact;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter (Collision col)
    {
        Health health = col.gameObject.GetComponent<Health>();
        if (health)
            health.Damage(damage);

        if (destroyOnImpact)
            Destroy(gameObject);
    }
}
