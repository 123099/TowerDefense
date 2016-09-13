using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour {

    [Tooltip("A pickup you want to spawn at the start of the level")]
    [SerializeField] private PickupData startPickup;
    [Tooltip("The time between pickup spawns in seconds")]
    [SerializeField] private float spawnTime;
    [Tooltip("Set to true if you want to allow more than 1 pickup to be alive at a time at this location")]
    [SerializeField] private bool allowMultiplePickups;
    [Tooltip("The value by which to multiply the stack size of the pickup")]
    [SerializeField] private float amountMultiplier;
    [Tooltip("A list of pickups which can be spawned at this location")]
    [SerializeField] private PickupData[] pickups;

    private RateTimer spawnTimer;
    private Pickup lastSpawned;

	private void Start () {
        SpawnPickup(startPickup);

        if(spawnTime != 0)
            spawnTimer = new RateTimer(1f / spawnTime, Time.time);
	}
	
	private void Update () {
        if (pickups.Length == 0)
            return;

        if (spawnTimer != null && spawnTimer.IsReady())
            if (allowMultiplePickups || !lastSpawned)
                SpawnRandomPickup();
	}

    public void SpawnRandomPickup ()
    {
        SpawnPickup(pickups[Random.Range(0, pickups.Length)]);
    }

    public void SpawnPickup(PickupData pickup)
    {
        if(pickup == null) return;

        if(lastSpawned != null)
            lastSpawned.OnCollect.RemoveAllListeners();
        lastSpawned = Instantiate(pickup.pickupModel, transform.position, transform.rotation) as Pickup;
        Vector3 pos = lastSpawned.transform.position;
        pos.z = 0;
        lastSpawned.transform.position = pos;
        lastSpawned.SetData(pickup, amountMultiplier);
        lastSpawned.OnCollect.AddListener(() => onLastSpawnedCollected());
    }

    public void SetAmountMultiplier(float amountMultiplier)
    {
        this.amountMultiplier = amountMultiplier;
    }

    public float GetAmountMultiplier ()
    {
        return amountMultiplier;
    }

    private void onLastSpawnedCollected ()
    {
        spawnTimer.SetLastReadyTime(Time.time);
    }
}
