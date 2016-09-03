using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour {

    private WizardBase wizardBase;
    protected Health target;

    private void Awake ()
    {
        wizardBase = FindObjectOfType<WizardBase>();
    }

    private void OnEnable ()
    {
        if (wizardBase)
        {
            LookAtBase();
            Move();
        }
        else
            Victory();
    }

    private void Update ()
    {
        if (target && target.IsAlive())
                Attack();

        if (wizardBase && !wizardBase.GetComponent<Health>().IsAlive())
            Victory();
    }

    private void LookAtBase ()
    {
        Quaternion rot = Quaternion.LookRotation(wizardBase.transform.position - transform.position);
        Vector3 eulerRot = rot.eulerAngles;
        eulerRot.x = eulerRot.z = 0;
        rot.eulerAngles = eulerRot;
        transform.rotation = rot;
    }

    private void Move ()
    {
        GetComponent<Animator>().SetFloat("Speed", 1);
    }

    private void Stop ()
    {
        GetComponent<Animator>().SetFloat("Speed", 0);
    }

    private void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.GetComponent<WizardBase>() == wizardBase)
            target = wizardBase.GetComponent<Health>();
        else if (col.gameObject.GetComponent<Wall>())
            target = col.gameObject.GetComponent<Health>();
        else if(col.gameObject.GetComponent<Player>())
        {
            Player player = col.gameObject.GetComponent<Player>();
            Vector3 dir = player.transform.position - transform.position;
            dir.y += 1;
            player.Knockback(dir.normalized * 400);
            player.Stun(1f);
        } 
    }

    public void Victory ()
    {
        Stop();
        wizardBase = null;
        target = null;
        //Dance
        print("Everybody dance now!");
    }

    public void DestroySelf ()
    {
        Destroy(gameObject);
    }

    public abstract void Attack ();
}
