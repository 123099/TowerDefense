using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {

    [SerializeField] private float meleeDamage;

    private WizardBase wizardBase;

    private void Awake ()
    {
        wizardBase = FindObjectOfType<WizardBase>();
    }

    private void OnEnable ()
    {
        Quaternion rot = Quaternion.LookRotation(wizardBase.transform.position - transform.position);
        Vector3 eulerRot = rot.eulerAngles;
        eulerRot.x = eulerRot.z = 0;
        rot.eulerAngles = eulerRot;
        transform.rotation = rot;

        GetComponent<Animator>().SetFloat("Speed", 1);
    }

    //Replace to enter and exit, which trigger attack animation, that has a damage event
    private void OnCollisionStay (Collision col)
    {
        if(col.gameObject.GetComponent<WizardBase>() == wizardBase)
            wizardBase.GetComponent<Health>().Damage(meleeDamage);
    }

    public void DestroySelf ()
    {
        Destroy(gameObject);
    }
}
