using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GameUtils
{
    public static Enemy[] GetAllEnemies ()
    {
        return GameObject.FindObjectsOfType<Enemy>();
    }

    public static Enemy GetNearestEnemyTo (Transform transform, float radius)
    {
        return getNearestEnemyFromList(GetAllEnemies(), transform, radius);
    }

    public static Enemy[] GetNearestEnemiesInFrontOf(Transform transform, float range, float amount)
    {
        float halfRange = range * 0.5f;
        Vector3 boxCenter = transform.position + transform.forward * halfRange;
        Vector3 halfExtents = new Vector3(halfRange, halfRange, halfRange);
        Collider[] collidersInFront = Physics.OverlapBox(boxCenter, halfExtents);

        List<Enemy> enemies = new List<Enemy>();

        foreach (Collider collider in collidersInFront)
        {
            if (collider.GetComponent<Enemy>())
            {
                if (enemies.Count < amount)
                    enemies.Add(collider.GetComponent<Enemy>());
                else
                    for(int i = 0; i < enemies.Count; ++i)
                    {
                        Enemy enemy = enemies[i];
                        if (Vector3.Distance(collider.transform.position, transform.position) < Vector3.Distance(enemy.transform.position, transform.position))
                        {
                            enemies[i] = collider.GetComponent<Enemy>();
                            break;
                        }
                    }
            }
        }

        return enemies.ToArray();
    }

    private static Enemy getNearestEnemyFromList(Enemy[] enemies, Transform transform, float distance)
    {
        Enemy nearestEnemy = null;
        foreach (Enemy enemy in enemies)
        {
            if (nearestEnemy == null)
            {
                nearestEnemy = enemy;
                continue;
            }

            float distanceToCurrentEnemy = ( transform.position - enemy.transform.position ).sqrMagnitude;
            float distanceToNearestEnemy = ( transform.position - nearestEnemy.transform.position ).sqrMagnitude;

            if (distanceToCurrentEnemy < distanceToNearestEnemy)
                nearestEnemy = enemy;
        }

        if (nearestEnemy == null)
            return null;

        float distanceToTarget = ( transform.position - nearestEnemy.transform.position ).sqrMagnitude;
        if (distanceToTarget <= distance * distance)
            return nearestEnemy;
        else
            return null;
    }

    public static Enemy[] GetAllEnemiesOfType(bool groundUnit)
    {
        Enemy[] enemies = GetAllEnemies();
        List<Enemy> typeEnemies = new List<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            if (enemy.IsGroundUnit() == groundUnit)
                typeEnemies.Add(enemy);
        }

        return typeEnemies.ToArray();
    }
}
