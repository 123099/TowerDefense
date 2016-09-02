using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Levels/Wave")]
public class Wave : ScriptableObject {

    #region UnitSetup
    [Tooltip("The position from which the units will spawn")]
    public Vector3 spawnPoint;
    [Tooltip("A list of all the units in this wave")]
    public Unit[] units;

    [Serializable]
    public class Unit
    {
        [Tooltip("The model representing the graphics and animations of the unit")]
        public Enemy modelPrefab;
        [Tooltip("An offset to move the unit away from the spawn position when spawning")]
        public Vector3 spawnOffset;
        [Tooltip("Time in seconds to wait after the previous unit has spawned to spawn this unit")]
        public float spawnDelayAfterLastUnit;
    }

    private List<Enemy> spawnedUnits;
    #endregion

    #region WaveLogic
    private int currentUnitIndex;
    private float timeOfLastSpawn;

    public void Initialize ()
    {
        currentUnitIndex = 0;
        timeOfLastSpawn = Time.time;
        spawnedUnits = new List<Enemy>();
    }

    public void Update ()
    {
        if (FinishedSpawning())
            return;

        if (Time.time >= timeOfLastSpawn + units[currentUnitIndex].spawnDelayAfterLastUnit)
            spawnNext();
    }

    private void spawnNext ()
    {
        spawnedUnits.Add(Instantiate(units[currentUnitIndex].modelPrefab, spawnPoint + units[currentUnitIndex].spawnOffset, Quaternion.identity) as Enemy);

        ++currentUnitIndex;
        timeOfLastSpawn = Time.time;
    }
    #endregion

    public bool FinishedSpawning ()
    {
        return currentUnitIndex >= units.Length;
    }

    public bool KilledAll ()
    {
        if (spawnedUnits.Count == 0)
            return false;

        foreach (Enemy enemy in spawnedUnits)
            if (enemy)
                return false;

        return true;
    }
}
