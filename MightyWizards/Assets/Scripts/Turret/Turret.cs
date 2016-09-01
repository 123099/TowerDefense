using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Turret : MonoBehaviour {

    //Add projectile usage using the Projectile ScriptableObject?
    [SerializeField]
    private float fireRate;
    private Enemy target=null;
    [SerializeField]
    private float fireRange;
    [SerializeField]
    private ProjectileData projectile;
    [SerializeField]
    private Transform fireLocation;

    private float timeSinceLastBullet=0f;

    void Update () {
        timeSinceLastBullet += Time.deltaTime;
        if (target) //Unity override boolean operations for MonoBehaviours, so that if(MonoBehaviour) = if (MonoBehaviour != null)
        {
            LookAtEnemy();
            if (timeSinceLastBullet >= 1f / fireRate)
            {
                timeSinceLastBullet = 0f;
                projectile.Launch(fireLocation);
            }

            if (target.GetComponent<Health>().GetHealth() <= 0)
                target = null;
        }
        else
            SetTarget(GameUtils.GetNearestEnemyTo(transform, fireRange));
    }

    public void SetTarget(Enemy target)
    {
        this.target = target;
    }

    private void LookAtEnemy()
    {
        transform.LookAt(target.transform);
    }
}
