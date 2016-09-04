﻿using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public class WizardBase : MonoBehaviour {

    [Tooltip("The wall model to spawn")]
    [SerializeField] private Wall wallPrefab;
    [Tooltip("The locations at which walls can spawn. Walls will spawn at the first free spot")]
    [SerializeField] private Transform[] wallSpawnPositions;

    private Wall[] spawnedWalls;

    private void Awake ()
    {
        spawnedWalls = new Wall[wallSpawnPositions.Length];
    }

    public bool SpawnWall ()
    {
        for(int i = 0; i < spawnedWalls.Length; ++i)
        {
            if(!spawnedWalls[i] || !spawnedWalls[i].GetComponent<Health>().IsAlive())
            {
                spawnedWalls[i] = Instantiate(wallPrefab, wallSpawnPositions[i].position, Quaternion.identity) as Wall;
                return true;
            }
        }

        return false;
    }

    public Wall GetWallPrefab ()
    {
        return wallPrefab;
    }
}
