using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ResourceInventory : MonoBehaviour {

    [SerializeField] private ResourceEvent OnResourceAdded;

    private Dictionary<PickupData, int> resources;

    private void Awake ()
    {
        resources = new Dictionary<PickupData, int>();
    }

	public void Add(PickupData pickup, int amount)
    {
        if (resources.ContainsKey(pickup))
            resources[pickup] += amount;
        else
            resources[pickup] = amount;

        OnResourceAdded.Invoke(pickup, amount);
    }

    public bool Has(PickupData pickup, int amount)
    {
        return resources.ContainsKey(pickup) && resources[pickup] >= amount;
    }
}
