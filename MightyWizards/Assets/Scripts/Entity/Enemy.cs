using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {

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
}
