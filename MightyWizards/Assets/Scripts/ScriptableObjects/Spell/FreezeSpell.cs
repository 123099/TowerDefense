using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Freeze")]
public class FreezeSpell : Spell
{
    [Tooltip("The amount of time to freeze ground enemies for")]
    public float freezeDuration;

    protected override void Effect ()
    {
        Enemy[] groundEnemies = GameUtils.GetAllEnemiesOfType(true);

        foreach (Enemy enemy in groundEnemies)
            enemy.Freeze(freezeDuration);
    }
}
