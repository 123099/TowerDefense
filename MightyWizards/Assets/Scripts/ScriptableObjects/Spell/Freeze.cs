using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Spells/Freeze")]
public class Freeze : Spell
{
    [Tooltip("The amount of time to freeze ground enemies for")]
    public float freezeDuration;

    protected override void Effect ()
    {
        Enemy[] groundEnemies = GameUtils.GetAllEnemiesOfType(true);

        foreach (Enemy enemy in groundEnemies)
        {
            enemy.Freeze(freezeDuration);

            GameObject freezeClone = Instantiate(spellPrefab, enemy.transform) as GameObject;

            float halfHeight = enemy.GetComponent<CapsuleCollider>().bounds.extents.y;

            freezeClone.transform.localPosition = new Vector3(0, halfHeight, 0);
            freezeClone.transform.localRotation = Quaternion.identity;

            Vector3 currentScale = freezeClone.transform.localScale;
            currentScale /= currentScale.y;
            currentScale *= halfHeight * 2;

            freezeClone.transform.localScale = currentScale;
            Destroy(freezeClone, freezeDuration);
        }
    }
}
