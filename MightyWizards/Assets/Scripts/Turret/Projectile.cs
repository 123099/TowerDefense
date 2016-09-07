using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour {

    private float damage;
    private bool aoe;
    private bool destroyOnImpact;

    public void SetDestroyOnImpact(bool destroyOnImpact)
    {
        this.destroyOnImpact = destroyOnImpact;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetAOE(bool aoe)
    {
        this.aoe = aoe;
    }

    private void OnCollisionEnter (Collision col)
    {
        if(!aoe && !gameObject.activeSelf) return;

        Health health = col.gameObject.GetComponent<Health>();
        if (health)
            health.Damage(damage);

        if (destroyOnImpact)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
