using UnityEngine;
using System.Collections;
using System;

public class RangedEnemy : Enemy {

    [Tooltip("The location from which the enemy shoots")]
    private Transform fireLocation;
    [Tooltip("The projectile this enemy shoots at it's target")]
    [SerializeField] private ProjectileData projectile;

    public override void Attack ()
    {
        projectile.Launch(fireLocation);
    }
}
