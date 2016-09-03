﻿using UnityEngine;
using System.Collections;

public class PickupSpawn : MonoBehaviour {

    [Tooltip("A pickup you want to spawn at the start of the level")]
    [SerializeField] private PickupData startPickup;
    [Tooltip("The time between pickup spawns in seconds")]
    [SerializeField] private float spawnTime;
    [Tooltip("Set to true if you want to allow more than 1 pickup to be alive at a time at this location")]
    [SerializeField] private bool allowMultiplePickups;
    [Tooltip("A list of pickups which can be spawned at this location")]
    [SerializeField] private PickupData[] pickups;

    private RateTimer spawnTimer;
    private Pickup lastSpawned;

	private void Start () {
        if (startPickup)
            SpawnPickup(startPickup);

        spawnTimer = new RateTimer(1f / spawnTime, Time.time);
	}
	
	void Update () {
        if (pickups.Length == 0)
            return;

        if (spawnTimer.IsReady())
            if(allowMultiplePickups || !lastSpawned)
                SpawnPickup(pickups[Random.Range(0, pickups.Length)]);
	}

    private void SpawnPickup(PickupData pickup)
    {
        lastSpawned = Instantiate(pickup.pickupModel, transform.position, transform.rotation) as Pickup;
        lastSpawned.SetData(pickup);
    }
}
