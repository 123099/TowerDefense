using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Projectiles/Projectile")]
public class ProjectileData : ScriptableObject {

    [Tooltip("The prefab with the model of the projectile and a rigidbody")]
    public Projectile projectilePrefab;
    [Tooltip("The force with which to fire the projectile forward")]
    public float launchForce;
    [Tooltip("The damage the projectile should deal to a health component upon impact")]
    public float damage;
    [Tooltip("The amount of seconds to keep the projectile in the game")]
    public float lifetime;
    [Tooltip("The rotation by which to offset the projectile's rotation to fire it in a different direction")]
    public Vector3 rotationOffset;
    [Tooltip("Set to true if you want the projectile to be able to be fired up and down")]
    public bool rotateAroundZX;
    [Tooltip("Set to true to destroy the projectile the moment it hits something")]
    public bool destroyUponImpact;

    public void Launch (Transform fireLocation)
    {
        Launch(fireLocation, fireLocation.rotation, false);
    }

    public void Launch(Transform fireLocation, Quaternion rotation, bool ignoreRotateAroundXZ = true)
    {
        Projectile projectile = Instantiate(projectilePrefab,
            fireLocation.position,
            rotation * Quaternion.Euler(rotationOffset)) as Projectile;

        if (!ignoreRotateAroundXZ && !rotateAroundZX)
        {
            Quaternion rot = projectile.transform.rotation;
            Vector3 eulerRot = rot.eulerAngles;
            eulerRot.x = eulerRot.z = 0;
            projectile.transform.rotation = Quaternion.Euler(eulerRot);
        }

        projectile.SetDestroyOnImpact(destroyUponImpact);
        projectile.SetDamage(damage);
        projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * launchForce);

        Destroy(projectile.gameObject, lifetime);
    }
}
