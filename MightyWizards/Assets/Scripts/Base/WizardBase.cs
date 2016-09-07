using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public class WizardBase : MonoBehaviour {

    [Tooltip("The wall model to spawn")]
    [SerializeField] private Wall wallPrefab;
    [Tooltip("The locations at which walls can spawn. Walls will spawn at the first free spot")]
    [SerializeField] private Transform[] wallSpawnPositions;
    [Tooltip("The locations at which turrets can spawn.")]
    [SerializeField] private TurretSlot[] turretSpawnPositions;

    private Wall[] spawnedWalls;

    private void Awake ()
    {
        spawnedWalls = new Wall[wallSpawnPositions.Length];
    }

    public bool SpawnWall ()
    {
        print("Wall spawned");
        for(int i = 0; i < spawnedWalls.Length; ++i)
        {
            if(!spawnedWalls[i] || !spawnedWalls[i].GetComponent<Health>().IsAlive())
            {
                spawnedWalls[i] = Instantiate(wallPrefab, wallSpawnPositions[i]) as Wall;
                spawnedWalls[i].transform.localPosition = Vector3.zero;
                spawnedWalls[i].transform.localRotation = Quaternion.identity;
                return true;
            }
        }

        return false;
    }

    public Wall GetWallPrefab ()
    {
        return wallPrefab;
    }

    public TurretSlot[] GetTurretSpawnPositions ()
    {
        return turretSpawnPositions;
    }

    public void SpawnTurret(Turret turret, int slotIndex)
    {
        SpawnTurret(turret, turretSpawnPositions[slotIndex]);
    }

    public void SpawnTurret(Turret turret, TurretSlot slot)
    {
        turret.transform.SetParent(slot.transform);
        turret.transform.localPosition = Vector3.zero;
        turret.transform.localRotation = Quaternion.identity;
    }
}
