using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Staffs/Staff")]
public class Staff : ScriptableObject {

    public GameObject modelPrefab;

    public GameObject basicAttack;
    public GameObject spell;

    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    private Transform fireLocation;

    public void Equip(Transform equipper)
    {
        GameObject staffPrefab = Instantiate(modelPrefab, equipper) as GameObject;
        staffPrefab.transform.position = equipper.position + locationOffset;
        staffPrefab.transform.rotation = equipper.rotation * Quaternion.Euler(rotationOffset);

        fireLocation = GameObject.FindGameObjectsWithTag("FireLocation")[0].transform;
    }

    public void Attack ()
    {
        GameObject projectile = Instantiate(basicAttack, fireLocation.position, fireLocation.rotation) as GameObject;
        Quaternion rot = projectile.transform.rotation;
        Vector3 eulerRot = rot.eulerAngles;
        eulerRot.x = eulerRot.z = 0;
        projectile.transform.rotation = Quaternion.Euler(eulerRot);
        projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 5000);
    }

    public void CastSpell ()
    {

    }
}
