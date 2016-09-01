using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Turret : MonoBehaviour {

    //Add projectile usage using the Projectile ScriptableObject?
    [SerializeField]
    private float fireRate;
    private Enemy target=null;
    [SerializeField]
    private float fireRange;
    [SerializeField]
    private ProjectileData projectile;
    [SerializeField]
    private Transform fireLocation;

    private float timeSinceLastBullet=0f;

    void Update () {
        timeSinceLastBullet += Time.deltaTime;
        if (timeSinceLastBullet >= 1f/fireRate)
        {
            timeSinceLastBullet = 0f;
            if (HasTarget())
            {
                LookAtEnemy();
                projectile.Launch(fireLocation);
                if (target.GetComponent<Health>().GetHealth() <= 0) //The health component has the GetHealth method, not the transform
                    target = null;
            }
            else
            {
                SetTarget(GetNearestTarget());
            }
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
        if (nearestEnemy == null)
            return null;

        float distanceToTarget = (transform.position - nearestEnemy.transform.position).sqrMagnitude;
        if (distanceToTarget <= fireRange*fireRange)
            return nearestEnemy;
        else
            return null;


    }
}
