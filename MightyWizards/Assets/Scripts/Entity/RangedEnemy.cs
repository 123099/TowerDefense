using UnityEngine;
using System.Collections;
using System;

public class RangedEnemy : Enemy {

    [Tooltip("The location from which the enemy shoots")]
    [SerializeField] private Transform fireLocation;
    [Tooltip("The projectile this enemy shoots at it's target")]
    [SerializeField] private ProjectileData projectile;
    [Tooltip("The fire rate timer")]
    [SerializeField] private RateTimer fireTimer;

    public override void Attack ()
    {
        if (fireTimer.IsReady())
        {
            Vector3 dir = target.transform.position - fireLocation.position;
            projectile.Launch(fireLocation, Quaternion.LookRotation(dir));
        }
    }
}
