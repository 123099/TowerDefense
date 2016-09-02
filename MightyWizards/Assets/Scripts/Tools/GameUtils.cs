using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GameUtils
{
    public static Enemy GetNearestEnemyTo (Transform transform, float radius)
    {
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        return getNearestEnemyFromList(allEnemies, transform, radius);
    }

    //TODO: Fix detection
    public static Enemy GetNearestEnemyInFrontOf(Transform transform, float range)
    {
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        List<Enemy> enemiesInFront = new List<Enemy>();

        foreach(Enemy enemy in allEnemies)
        {
            float angle = Vector3.Angle(enemy.transform.position - transform.position, transform.forward);
            if (angle <= 30 && angle >= -30)
                enemiesInFront.Add(enemy);
        }

        return getNearestEnemyFromList(enemiesInFront.ToArray(), transform, range);
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
}
