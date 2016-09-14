using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public class Wall : MonoBehaviour {

    [Tooltip("The resource used to build this wall")]
    [SerializeField] private PickupData resourceType;
    [Tooltip("The amount of the resource required to build this wall")]
    [SerializeField] private int cost;

    private bool spawning;

    private List<Collider> knockbackedUnits;

    private void OnEnable ()
    {
        spawning = true;
        knockbackedUnits = new List<Collider>();
        Invoke("spawned", 1);
    }

    private void spawned ()
    {
        spawning = false;
    }

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

    private void OnCollisionEnter(Collision c)
    {
        if (spawning)
        {
            if (!knockbackedUnits.Contains(c.collider) && c.collider.GetComponent<Enemy>())
            {
                c.collider.GetComponent<Enemy>().Freeze(1f);
                c.collider.GetComponent<Rigidbody>().AddForce(-transform.forward * 10000);
                knockbackedUnits.Add(c.collider);
            }
        }
    }
}
