using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Staffs/Staff")]
public class Staff : ScriptableObject {

    [Tooltip("The model of the staff. It must have a child transform tagged as FireLocation, marking from where projectiles will fire")]
    public GameObject modelPrefab;

    [Tooltip("The projectile this staff will fire")]
    public ProjectileData basicAttack;
    [Tooltip("The spell that goes with this staff")]
    public GameObject spell;
    [Tooltip("The auto aim range")]
    public float autoAimRadius;

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
    }

    public void Attack ()
    {
        Enemy nearestEnemy = GameUtils.GetNearestEnemyInFrontOf(fireLocation, autoAimRadius);
        if (nearestEnemy)
        {
            Quaternion rotation = Quaternion.LookRotation(nearestEnemy.transform.position - fireLocation.transform.position);
            basicAttack.Launch(fireLocation, rotation);
        }
        else
            basicAttack.Launch(fireLocation);
    }

    public void CastSpell ()
    {
        Instantiate(spell, fireLocation.position, Quaternion.identity);
    }
}
