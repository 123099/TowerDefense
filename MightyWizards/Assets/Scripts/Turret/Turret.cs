using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Turret : MonoBehaviour {

    //Add projectile usage using the Projectile ScriptableObject?
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Transform target=null; //Maybe change to being Enemy, since turrets only target the enemies
    [SerializeField]
    private float fireRange;

	void Start () {
        SetTarget(FindObjectOfType<Enemy>().transform); //Change to finding the nearest enemy, not the first enemy in the scene. Should create a GetNearestEnemy private method
    }
	
	void Update () {
        if (HasTarget())
        {
            //shoot in it's direction
            //check if it is still alive, if not, set target to null
            if (target.GetComponent<Health>().GetHealth()<=0) //The health component has the GetHealth method, not the transform
                target = null;
        }
        else
        {
            SetTarget(FindObjectOfType<Enemy>().transform);
        }

    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public bool HasTarget()
    {
        if (target != null)
            return true;
        else
            return false;
    }
}
