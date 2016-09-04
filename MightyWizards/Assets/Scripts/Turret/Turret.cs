using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Turret : MonoBehaviour {

    [Tooltip("The rate timer for firing the turret")]
    [SerializeField] private RateTimer fireTimer;
    [Tooltip("The maximum range at which the turret will shoot enemies")]
    [SerializeField] private float fireRange;

    [Tooltip("The projectile this turret will shoot")]
    [SerializeField] private ProjectileData projectile;
    [Tooltip("The location from which the projectile will fire")]
    [SerializeField] private Transform fireLocation;

    private Enemy target=null;

    void Update () {
        if (IsTargetInRange())
        {
            LookAtEnemy();

            if (fireTimer.IsReady())
                projectile.Launch(fireLocation);

            if (!target.GetComponent<Health>().IsAlive())
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

    private bool IsTargetInRange ()
    {
        return target && Vector3.Distance(target.transform.position, transform.position) <= fireRange;
    }

    public ProjectileData GetProjectile ()
    {
        return projectile;
    }
}
