using UnityEngine;
using System.Collections;

public class GolemAnimationEvent : MonoBehaviour {

    private Enemy enemy;

    private void Awake ()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

	public void Attack ()
    {
        enemy.DamageTarget();
    }

    public void Die ()
    {
        enemy.DestroySelf();
    }

    public void SpawnProjectile ()
    {
        enemy.SpawnProjectile();
    }

    public void ShootProjectile ()
    {
        enemy.ShootProjectile();
    }
}
