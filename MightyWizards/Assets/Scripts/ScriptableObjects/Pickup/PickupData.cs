using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Pickups/Pickup")]
public class PickupData : ScriptableObject {
    [Tooltip("The prefab for this pickup with a pickup component on it")]
    public Pickup pickupModel;
    [Tooltip("How many resources this pickup adds to the resource inventory when it is collected")]
    public int stackAmount;
}
