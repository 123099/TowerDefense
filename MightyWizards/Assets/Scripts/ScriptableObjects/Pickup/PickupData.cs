using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Pickups/Pickup")]
public class PickupData : ScriptableObject {
    public Pickup pickupModel;
    public int stackAmount;
}
