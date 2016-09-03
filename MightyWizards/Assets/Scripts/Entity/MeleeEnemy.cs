using UnityEngine;
using System.Collections;
using System;

public class MeleeEnemy : Enemy {

    [SerializeField] private float damage; //Maybe replace with weapon scriptable object?

    public override void Attack ()
    {
        target.Damage(damage);
    }
}
