using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    private PickupData pickupData;

    public void SetData(PickupData pickupData)
    {
        this.pickupData = pickupData;
    }

	public void Collect(ResourceInventory inventory)
    {
        inventory.Add(pickupData, pickupData.stackAmount);
        Destroy(gameObject);
    }
}
