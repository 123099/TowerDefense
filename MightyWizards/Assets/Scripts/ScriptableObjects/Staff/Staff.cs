using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Staffs/Staff")]
public class Staff : ScriptableObject {

    [Tooltip("The model of the staff. It must have a child transform tagged as FireLocation, marking from where projectiles will fire")]
    public GameObject modelPrefab;

    [Tooltip("The projectile this staff will fire")]
    public ProjectileData basicAttack;
    [Tooltip("The spell that goes with this staff")]
    public Spell spell;
    [Tooltip("The amount by which to multiply the damage of all basic attacks")]
    public float damageMultiplier;
    [Tooltip("The auto aim range")]
    public float autoAimRadius;
    [Tooltip("The maximum amount of enemies this staff attacks at once")]
    public int maxEnemiesHit;

    [Tooltip("The offset to move the staff from the spawn location")]
    public Vector3 locationOffset;
    [Tooltip("The offset to rotate the staff around itself")]
    public Vector3 rotationOffset;

    private Transform fireLocation;

    public void Equip(Transform equipper)
    {
        GameObject staffPrefab = Instantiate(modelPrefab, equipper) as GameObject;
        staffPrefab.transform.position = equipper.position + locationOffset;
        staffPrefab.transform.rotation = equipper.rotation * Quaternion.Euler(rotationOffset);

        fireLocation = GameObjectExtension.FindObjectWithTagIn(staffPrefab, "FireLocation").transform;

        spell.Initialize();
    }

    public void Attack ()
    {
        Enemy[] nearestEnemies = GameUtils.GetNearestEnemiesInFrontOf(fireLocation, autoAimRadius, 2, maxEnemiesHit);

        if (nearestEnemies.Length > 0)
        {
            foreach (Enemy enemy in nearestEnemies)
            {
                Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - fireLocation.transform.position);
                Projectile projectile = basicAttack.Launch(fireLocation, rotation);
                projectile.SetDamage(basicAttack.damage * damageMultiplier);
            }
        }
        else
        {
            Projectile projectile = basicAttack.Launch(fireLocation);
            projectile.SetDamage(basicAttack.damage * damageMultiplier);
        }

    }

    public void CastSpell ()
    {
        spell.Activate();
    }
}
