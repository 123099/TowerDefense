using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(ResourceInventory))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour {

    [Tooltip("The staff this player has equipped")]
    [SerializeField] private Staff staff;
    [Tooltip("The position and rotation at which staffs spawn")]
    [SerializeField] private Transform staffSpawn;

    public UnityEvent OnLand;

    private ResourceInventory resourceInventory;
    private Rigidbody rigidbody;
    private Collider collider;

    private float halfHeight;

    private bool isStunned;
    private bool isGrounded;

    private Collider ground;

    private void Awake ()
    {
        resourceInventory = GetComponent<ResourceInventory>();
        rigidbody = GetComponent<Rigidbody>();

        collider = GetComponent<Collider>();
        halfHeight = collider.bounds.extents.y;
    }

    private void OnEnable ()
    {
        SetStaff(staff);
    }

    private void Update ()
    {
        if(isStunned) return;
        if(GameUtils.IsGamePaused()) return;

        CheckGrounded();
        Attack();
        BuildWall();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Pickup>())
            col.GetComponent<Pickup>().Collect(resourceInventory);
    }

    private void BuildWall ()
    {
        if (Input.GetButtonDown("Build"))
        {
            WizardBase wizardBase = GameUtils.GetBase();
            if(!wizardBase) return;

            Wall wallPrefab = wizardBase.GetWallPrefab();
            if (resourceInventory.Has(wallPrefab.GetResourceType(), wallPrefab.GetCost()))
            {
                if(wizardBase.SpawnWall())
                    resourceInventory.Add(wallPrefab.GetResourceType(), -wallPrefab.GetCost());
            }
        }
    }

    private void Attack ()
    {
        if (Input.GetButtonDown("Fire"))
            staff.Attack();
    }

    private void CheckGrounded ()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if(Physics.SphereCast(ray, 0.5f, out hit, halfHeight, LayerMask.GetMask("Ground", "Platform", "Wall")))
        {
            if (!isGrounded)
                OnLand.Invoke();

            isGrounded = true;

            ground = hit.collider;
        }
        else
        {
            isGrounded = false;
            ground = null;
        }
    }

    public void CastSpell ()
    {
        staff.CastSpell();
    }

    public Collider GetGround ()
    {
        return ground;
    }

    public bool IsGrounded ()
    {
        return isGrounded;
    }

    public bool IsStunned ()
    {
        return isStunned;
    }

    public void SetStaff(Staff staff)
    {
        this.staff = staff;
        staff.Equip(staffSpawn);
    }

    public Staff GetStaff ()
    {
        return staff;
    }

    public void Knockback(Vector3 force)
    {
        rigidbody.AddForce(force);
    }

    public void Stun(float duration)
    {
        if (!isStunned)
            StartCoroutine(stun(duration));
    }

    private IEnumerator stun(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
}
