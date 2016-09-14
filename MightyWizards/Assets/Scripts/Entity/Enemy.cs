using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

    public Animator animator;
    public float moveSpeed;
    public float attackRange;
    public float damage;
    public float rangeThreshold;
    public bool isGroundUnit;
    public ProjectileData projectileData;
    public Transform fireLocation;

    private Projectile spawnedProjectile;

    private Rigidbody rigidbody;
    private WizardBase wizardBase;

    private Health target;
    private float halfHeight;

    private bool won;
    private bool frozen;

    private void OnEnable ()
    {
        rigidbody = GetComponent<Rigidbody>();
        wizardBase = GameUtils.GetBase();

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        animator.SetBool("Ranged", attackRange >= rangeThreshold);

        rigidbody.useGravity = isGroundUnit;

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
        if(!wizardBase) Win();

        if(won || frozen) return;

        target = GetTarget();

        if (target)
        {
            if (target.GetComponent<WizardBase>())
            {
                Health t = GetTarget();
                if (t && t.GetComponent<Wall>() && t.IsAlive())
                    target = t;
            }

            if (target.IsAlive())
            {
                Attack();
            }
        }
        else
        {
            Move();
        }
    }

    private Health GetTarget ()
    {
        Wall[] wallsInFront = GameUtils.GetNearestObjectsInFrontOf<Wall>(transform, Vector3.up * halfHeight, attackRange, halfHeight, 5);
        WizardBase[] basesInFront = GameUtils.GetNearestObjectsInFrontOf<WizardBase>(transform, Vector3.up * halfHeight, attackRange, halfHeight, 5);

        if (wallsInFront != null && wallsInFront.Length > 0)
            return wallsInFront[0].GetComponent<Health>();
        else if (basesInFront != null && basesInFront.Length > 0)
            return basesInFront[0].GetComponent<Health>();
        
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

    private void SetVelocity(float velocity)
    {
        Vector3 vel = rigidbody.velocity;
        vel.x = velocity * Mathf.Sign(transform.forward.x);
        rigidbody.velocity = vel;
    }

    private void Move ()
    {
        animator.SetBool("Attacking", false);
        animator.SetFloat("Speed", 1);
        SetVelocity(moveSpeed);
    }

    private void Stop ()
    {
        animator.SetBool("Attacking", false);
        animator.SetFloat("Speed", 0);
        SetVelocity(0);
    }

    private void Attack ()
    {
        animator.SetBool("Attacking", true);
        SetVelocity(0);
    }

    private void Win ()
    {
        Stop();
        target = null;
        won = true;
    }

    public void Die ()
    {
        SetVelocity(0);
        frozen = true;
        animator.SetBool("Die", true);
    }

    public void DamageTarget ()
    {
        if(target)
            target.Damage(damage);
    }

    public void DestroySelf ()
    {
        if (isGroundUnit)
            StartCoroutine(destroySelf());
        else
            Destroy(gameObject);
    }

    private IEnumerator destroySelf ()
    {
        Vector3 start = transform.position;
        Vector3 destination = start + 0.5f*Vector3.down;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            transform.position = Vector3.Slerp(start, destination, t);
            yield return 0;
        }
        Destroy(gameObject);
}

    public void SpawnProjectile ()
    {
        if (projectileData)
        {
            spawnedProjectile = projectileData.SpawnProjectile(fireLocation);
            spawnedProjectile.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void ShootProjectile ()
    {
        if (spawnedProjectile)
        {
            spawnedProjectile.GetComponent<Rigidbody>().isKinematic = false;
            projectileData.Launch(spawnedProjectile, Quaternion.LookRotation(transform.forward));
        }
    }

    public bool IsGroundUnit ()
    {
        return isGroundUnit;
    }

    private void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.GetComponent<Player>())
        {
            Player player = col.gameObject.GetComponent<Player>();
            Vector3 dir = player.transform.position - transform.position;
            Vector3 force = Mathf.Sign(dir.x) * Vector3.right + Vector3.up * 0.5f;
            force *= 40000;

            player.Knockback(force);
            player.Stun(1f);
        } 
    }

    public void Freeze(float duration)
    {
        StartCoroutine(freeze(duration));
    }

    private IEnumerator freeze(float duration)
    {
        Stop();
        frozen = true;
        animator.speed = 0;
        yield return new WaitForSeconds(duration);
        animator.speed = 1;
        Move();
        frozen = false;
    }

    public void OnModelSet(GameObject model)
    {
        animator = model.GetComponent<Animator>();
        if (!fireLocation)
        {
            fireLocation = GameUtils.FindRecursively(model.transform, "R_Pinky_Joint3");
        }
    }
}
