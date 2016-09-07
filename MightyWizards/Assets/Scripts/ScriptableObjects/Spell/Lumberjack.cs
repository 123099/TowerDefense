using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Lumberjack")]
public class Lumberjack : Spell {

    [Tooltip("The amount to heal the base and walls by")]
    public float healAmount;
    [Tooltip("The amount of walls to build")]
    [SerializeField] [Range(1,4)] private int wallsToBuild = 1;

    protected override void Effect ()
    {
        GameUtils.GetBase().GetComponent<Health>().Heal(healAmount);

        /**Wall[] walls = GameUtils.GetAllWalls();
        foreach (Wall wall in walls)
            wall.GetComponent<Health>().Heal(healAmount);*/

        WizardBase wizardBase = GameUtils.GetBase();
        for (int i = 0; i < wallsToBuild; ++i)
            wizardBase.SpawnWall();
    }
}
