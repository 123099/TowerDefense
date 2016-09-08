using UnityEngine;
using System.Collections;
using System;

public class MeleeEnemy : Enemy {

    [Tooltip("The damage the enemy deals with every attack")]
    [SerializeField] private float damage; //Maybe replace with weapon scriptable object?

    public override void Attack ()
    {
        if(!target) return;
        target.Damage(damage);
    }
}
