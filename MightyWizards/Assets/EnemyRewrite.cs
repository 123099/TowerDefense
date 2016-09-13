using UnityEngine;
using System.Collections;

public class EnemyRewrite : MonoBehaviour {

    public Animator animator;
    public float moveSpeed;
    public float attackRange;
    public float damage;
    public float rangeThreshold;
    
    private Rigidbody rigidbody;
    private WizardBase wizardBase;

    private Health target;
    private float halfHeight;

    private void OnEnable ()
    {
        rigidbody = GetComponent<Rigidbody>();
        wizardBase = GameUtils.GetBase();

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        if (attackRange >= rangeThreshold)
            animator.SetBool("Ranged", true);

        if (wizardBase)
        {
            LookAtBase();
            Move();
        }
        else
        {
            Win();
        }
    }

    private void Update ()
    {
        if(target)
        {
            if (target.IsAlive())
            {
                Attack();
            }
            else if (target.GetComponent<WizardBase>())
            {   
                Win();
            }
        }
        else
        {
            target = GetTarget();
            Move();
        }
    }

    private Health GetTarget ()
    {
        Health[] objectsInFront = GameUtils.GetNearestObjectsInFrontOf<Health>(transform, Vector3.up * halfHeight, attackRange, halfHeight, 10);

        if (objectsInFront != null && objectsInFront.Length > 0 && ( objectsInFront[0].GetComponent<Wall>() || objectsInFront[0].GetComponent<WizardBase>() ))
            return objectsInFront[0];
        else
            return null;
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
        animator.SetBool("Attacking", false);
        animator.SetFloat("Speed", 1);
        rigidbody.velocity = moveSpeed * Vector3.right;
    }

    private void Stop ()
    {
        animator.SetBool("Attacking", false);
        animator.SetFloat("Speed", 0);
        rigidbody.velocity = Vector3.zero;
    }

    private void Attack ()
    {
        animator.SetBool("Attacking", true);
        rigidbody.velocity = Vector3.zero;
    }

    private void Win ()
    {
        Stop();
    }

    public void Die ()
    {
        animator.SetTrigger("Die");
    }

    public void DamageTarget ()
    {
        target.Damage(damage);
    }

    public void DestroySelf ()
    {
        Destroy(gameObject);
    }
}
