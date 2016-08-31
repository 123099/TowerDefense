using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Waves/Wave")]
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
        public GameObject modelPrefab;
        [Tooltip("An offset to move the unit away from the spawn position when spawning")]
        public Vector3 spawnOffset;
        [Tooltip("Time in seconds to wait after the previous unit has spawned to spawn this unit")]
        public float spawnDelayAfterLastUnit;
    }
    #endregion

    #region WaveLogic
    private int currentUnitIndex;
    private float timeOfLastSpawn;

    private bool complete;

    public void Initialize ()
    {
        complete = false;

        if (units.Length == 0)
        {
            complete = true;
            return;
        }

        currentUnitIndex = 0;
        timeOfLastSpawn = Time.time;
    }

    public void Update ()
    {
        if (currentUnitIndex >= units.Length)
            complete = true;

        if (complete)
            return;

        if (Time.time >= timeOfLastSpawn + units[currentUnitIndex].spawnDelayAfterLastUnit)
            spawnNext();
    }

    private void spawnNext ()
    {
        Instantiate(units[currentUnitIndex].modelPrefab, spawnPoint + units[currentUnitIndex].spawnOffset, Quaternion.identity);

        ++currentUnitIndex;
        timeOfLastSpawn = Time.time;
    }
    #endregion

    public bool IsComplete ()
    {
        return complete;
    }
}
