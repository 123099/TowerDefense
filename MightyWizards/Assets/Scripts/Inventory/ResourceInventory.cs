using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceInventory : MonoBehaviour {

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
    }
}
