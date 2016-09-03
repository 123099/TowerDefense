using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Turret : MonoBehaviour {

    [SerializeField]
    private RateTimer fireTimer;
    [SerializeField]
    private float fireRange;

    [SerializeField]
    private ProjectileData projectile;
    [SerializeField]
    private Transform fireLocation;

    private Enemy target=null;

    void Update () {
        if (target)
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
}
