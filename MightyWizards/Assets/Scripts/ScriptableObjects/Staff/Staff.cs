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

    public GameObject Equip(Transform equipper)
    {
        GameObject staffPrefab = Instantiate(modelPrefab, equipper) as GameObject;
        staffPrefab.transform.position = equipper.position + locationOffset;
        staffPrefab.transform.rotation = equipper.rotation * Quaternion.Euler(rotationOffset);

        spell.Initialize();

        return staffPrefab;
    }

    public void Attack (Transform fireLocation)
    {
        Enemy[] nearestEnemies = GameUtils.GetNearestObjectsInFrontOf<Enemy>(fireLocation, Vector3.down, autoAimRadius, 3, 999);

        if (nearestEnemies.Length > 0)
        {
            int shots = 0;
            for (int i = 0; i < nearestEnemies.Length && shots < maxEnemiesHit; ++i)
            {
                Enemy enemy = nearestEnemies[i];

                if (!enemy.GetComponent<Health>().IsAlive())
                    continue;

                Quaternion rotation = Quaternion.LookRotation(enemy.transform.position + Vector3.up - fireLocation.transform.position);
                Projectile projectile = basicAttack.SpawnProjectile(fireLocation, rotation);
                basicAttack.Launch(projectile);
                projectile.SetDamage(basicAttack.damage * damageMultiplier);
                ++shots;
            }
        }
        else
        {
            Projectile projectile = basicAttack.SpawnProjectile(fireLocation);
            basicAttack.Launch(projectile);
            projectile.SetDamage(basicAttack.damage * damageMultiplier);
        }
    }

    public void CastSpell ()
    {
        spell.Activate();
    }
}
