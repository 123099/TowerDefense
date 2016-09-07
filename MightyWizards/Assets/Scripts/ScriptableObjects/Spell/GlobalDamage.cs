using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Global Damage")]
public class GlobalDamage : Spell {

    [Tooltip("Damage to deal to the enemies")]
    [SerializeField] private float damage;
    [Tooltip("Set to true if should damage ground units")]
    [SerializeField] private bool damageGroundUnits;
    [Tooltip("Set to true if should damage air units")]
    [SerializeField] private bool damageAirUnits;

    protected override void Effect ()
    {
        if (damageGroundUnits)
        {
            Enemy[] enemies = GameUtils.GetAllEnemiesOfType(true);

            foreach (Enemy enemy in enemies)
                enemy.GetComponent<Health>().Damage(damage);
        }

        if (damageAirUnits)
        {
            Enemy[] enemies = GameUtils.GetAllEnemiesOfType(false);

            foreach (Enemy enemy in enemies)
                enemy.GetComponent<Health>().Damage(damage);
        }
    }
}
