using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Hellstorm")]
public class Hellstorm : Spell {

    [Tooltip("Damage to deal to all the enemies")]
    public float damage;

    public override void Activate ()
    {
        Enemy[] enemies = GameUtils.GetAllEnemies();

        foreach (Enemy enemy in enemies)
            enemy.GetComponent<Health>().Damage(damage);
    }
}
