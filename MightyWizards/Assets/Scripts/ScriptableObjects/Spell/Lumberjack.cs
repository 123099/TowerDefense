using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Lumberjack")]
public class Lumberjack : Spell {

    [Tooltip("The amount to heal the base and walls by")]
    public float healAmount;

    protected override void Effect ()
    {
        GameUtils.GetBase().GetComponent<Health>().Heal(healAmount);

        Wall[] walls = GameUtils.GetAllWalls();
        foreach (Wall wall in walls)
            wall.GetComponent<Health>().Heal(healAmount);
    }
}
