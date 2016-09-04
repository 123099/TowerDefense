using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Pickup : MonoBehaviour {

    public UnityEvent OnCollect;

    private PickupData pickupData;

    public void SetData(PickupData pickupData)
    {
        this.pickupData = pickupData;
    }

	public void Collect(ResourceInventory inventory)
    {
        inventory.Add(pickupData, pickupData.stackAmount);
        OnCollect.Invoke();
        Destroy(gameObject);
    }
}
