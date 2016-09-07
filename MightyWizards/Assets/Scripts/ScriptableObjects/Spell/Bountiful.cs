using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Bountiful")]
public class Bountiful : Spell
{
    [Tooltip("The resource multiplier to set to the enemies")]
    [SerializeField] private float resourceMultiplier;

    private float previousResourceMultiplier;

    public override void PassiveStart ()
    {
        previousResourceMultiplier = GameUtils.GetAllEnemies()[0].GetComponent<PickupSpawner>().GetAmountMultiplier();
    }

    protected override void Effect ()
    {
        Enemy[] enemies = GameUtils.GetAllEnemies();
        foreach (Enemy enemy in enemies)
            enemy.GetComponent<PickupSpawner>().SetAmountMultiplier(resourceMultiplier);
    }

    public override void PassiveStop ()
    {
        Enemy[] enemies = GameUtils.GetAllEnemies();
        foreach (Enemy enemy in enemies)
            enemy.GetComponent<PickupSpawner>().SetAmountMultiplier(previousResourceMultiplier);
    }
}
