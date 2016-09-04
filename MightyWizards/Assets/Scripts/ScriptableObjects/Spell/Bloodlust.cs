using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Bloodlust")]
public class Bloodlust : Spell {

    [Tooltip("The percentage by which to increase your and the turrets' damage")]
    public float damageIncrease;

    protected override void Effect ()
    {
        Turret[] turrets = GameUtils.GetAllTurrets();

        foreach (Turret turret in turrets)
            turret.GetProjectile().damage *= 1f + damageIncrease;

        GameUtils.GetPlayer().GetStaff().basicAttack.damage *= 1f + damageIncrease;
    }
}
