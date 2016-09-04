using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public class Wall : MonoBehaviour {

    [Tooltip("The resource used to build this wall")]
    [SerializeField] private PickupData resourceType;
    [Tooltip("The amount of the resource required to build this wall")]
    [SerializeField] private int cost;

    public PickupData GetResourceType ()
    {
        return resourceType;
    }

    public int GetCost ()
    {
        return cost;
    }

    public void DestroySelf ()
    {
        Destroy(gameObject);
    }
}
