using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Turret : MonoBehaviour {

    //Add projectile usage using the Projectile ScriptableObject?
    [SerializeField]
    private float fireRate;
    private Enemy target=null; //Maybe change to being Enemy, since turrets only target the enemies
    [SerializeField]
    private float fireRange;
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private Transform fireLocation;
	
	void Update () {
        if (HasTarget())
        {
            LookAtEnemy();
            projectile.Launch(fireLocation);
            if (target.GetComponent<Health>().GetHealth()<=0) //The health component has the GetHealth method, not the transform
                target = null;
        }
        else
        {
            SetTarget(GetNearestTarget());
        }



    }

    public void SetTarget(Enemy target)
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

    private void LookAtEnemy()
    {
        transform.LookAt(target.transform);
    }

    private Enemy GetNearestTarget()
    {
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        Enemy nearestEnemy = null;
        foreach(Enemy enemy in allEnemies)
        {
            if (nearestEnemy == null)
            {
                nearestEnemy = enemy;
                continue;
            }
            float distanceToCurrentEnemy = (transform.position - enemy.transform.position).sqrMagnitude;
            float distanceToNearestEnemy = (transform.position - nearestEnemy.transform.position).sqrMagnitude;

            if (distanceToCurrentEnemy < distanceToNearestEnemy)
                nearestEnemy = enemy;
        }
        return nearestEnemy;


    }
}
