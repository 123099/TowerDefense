﻿using System;
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

    public static Turret[] GetAllTurrets ()
    {
        return GameObject.FindObjectsOfType<Turret>();
    }

    public static Player GetPlayer ()
    {
        return GameObject.FindObjectOfType<Player>();
    }

    public static WizardBase GetBase ()
    {
        return GameObject.FindObjectOfType<WizardBase>();
    }

    public static Wall[] GetAllWalls ()
    {
        return GameObject.FindObjectsOfType<Wall>();
    }

    public static Enemy GetNearestEnemyTo (Transform transform, float radius)
    {
        return getNearestEnemyFromList(GetAllEnemies(), transform, radius);
    }

    public static T[] GetNearestObjectsInFrontOf<T>(Transform transform, float range, float halfHeight, int amount) where T : MonoBehaviour
    {
        float halfRange = range * 0.5f;
        Vector3 forward = transform.forward;
        forward.y = 0;
        
        Vector3 boxCenter = transform.position + forward.normalized * halfRange;
        Vector3 halfExtents = new Vector3(halfRange, halfHeight, halfRange);

        Collider[] collidersInFront = Physics.OverlapBox(boxCenter, halfExtents);

        List<T> objects = new List<T>();

        foreach (Collider collider in collidersInFront)
        {
            if (collider.GetComponent<T>() != null)
            {
                if (objects.Count < amount)
                    objects.Add(collider.GetComponent<T>());
                else
                    for(int i = 0; i < objects.Count; ++i)
                    {
                        T obj = objects[i];
                        if (Vector3.Distance(collider.transform.position, transform.position) < Vector3.Distance(obj.transform.position, transform.position))
                        {
                            objects[i] = collider.GetComponent<T>();
                            break;
                        }
                    }
            }
        }

        return objects.ToArray();
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

    public static bool IsGamePaused ()
    {
        return Time.timeScale == 0;
    }
}
