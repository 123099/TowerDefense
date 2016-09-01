using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Projectiles/Projectile")]
public class Projectile : ScriptableObject {

    [Tooltip("The prefab with the model of the projectile and a rigidbody")]
    public Rigidbody projectilePrefab;
    [Tooltip("The force with which to fire the projectile forward")]
    public float launchForce;
    [Tooltip("The amount of seconds to keep the projectile in the game")]
    public float lifetime;
    [Tooltip("The rotation by which to offset the projectile's rotation to fire it in a different direction")]
    public Vector3 rotationOffset;

    public void Launch (Transform fireLocation)
    {
        Rigidbody projectile = Instantiate(projectilePrefab,
            fireLocation.position,
            fireLocation.rotation * Quaternion.Euler(rotationOffset)) as Rigidbody;

        Quaternion rot = projectile.transform.rotation;
        Vector3 eulerRot = rot.eulerAngles;
        eulerRot.x = eulerRot.z = 0;
        projectile.transform.rotation = Quaternion.Euler(eulerRot);

        projectile.AddRelativeForce(Vector3.forward * launchForce);

        Destroy(projectile.gameObject, lifetime);
    }
}
