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
            turret.SetDamageMultiplier( 1f + damageIncrease );

        //Clone the staff so that value changes don't stay
        Staff staffClone = GameObject.Instantiate(GameUtils.GetPlayer().GetStaff());
        staffClone.damageMultiplier = 1f + damageIncrease;
        GameUtils.GetPlayer().SetStaff(staffClone);
    }
}
