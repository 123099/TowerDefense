using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Pickup : MonoBehaviour {

    public UnityEvent OnCollect;

    private PickupData pickupData;
    private float amountMultiplier;

    public void SetData(PickupData pickupData, float amountMultiplier)
    {
        this.pickupData = pickupData;
        this.amountMultiplier = amountMultiplier;
    }

	public void Collect(ResourceInventory inventory)
    {
        inventory.Add(pickupData, (int)(pickupData.stackAmount * amountMultiplier));
        OnCollect.Invoke();
        Destroy(gameObject);
    }
}
