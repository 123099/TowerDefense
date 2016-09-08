using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Turret : MonoBehaviour {

    [Tooltip("The part of the turret that should look at the enemy")]
    [SerializeField] private Transform rotatingPiece;
    [Tooltip("The turret's animator")]
    [SerializeField] private Animator turretAnimator;
    [Tooltip("The maximum range at which the turret will shoot enemies")]
    [SerializeField] private float fireRange;
    [Tooltip("The amount by which to multiply the damage of all basic attacks")]
    [SerializeField] private float damageMultiplier;
    [Tooltip("Do the projectiles of this turret hit multiples enemies at once or not")]
    [SerializeField] private bool aoe;

    [Tooltip("The projectile this turret will shoot")]
    [SerializeField] private ProjectileData projectile;
    [Tooltip("The location from which the projectile will fire")]
    [SerializeField] private Transform fireLocation;

    private Enemy target=null;

    void Update () {
        if (IsTargetInRange())
        {
            LookAtEnemy();
            turretAnimator.SetBool("Attacking", true);

            if (!target.GetComponent<Health>().IsAlive())
            {
                target = null;
                turretAnimator.SetBool("Attacking", false);
            }
        }
        else
            SetTarget(GameUtils.GetNearestEnemyTo(transform, fireRange));
    }

    public void SetTarget(Enemy target)
    {
        this.target = target;
    }

    public float GetDamageMultiplier ()
    {
        return damageMultiplier;
    }

    public void SetDamageMultiplier (float damageMultiplier)
    {
        this.damageMultiplier = damageMultiplier;
    }

    public void Shoot ()
    {
        Projectile proj = projectile.Launch(fireLocation);
        proj.SetDamage(projectile.damage * damageMultiplier);
        proj.SetAOE(aoe);
    }

    private void LookAtEnemy()
    {
        Vector3 dir = target.transform.position - rotatingPiece.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        rotatingPiece.rotation = Quaternion.Slerp(rotatingPiece.rotation, rotation, 0.5f);
    }

    private bool IsTargetInRange ()
    {
        return target && Vector3.Distance(target.transform.position, transform.position) <= fireRange;
    }

    public ProjectileData GetProjectile ()
    {
        return projectile;
    }
}
