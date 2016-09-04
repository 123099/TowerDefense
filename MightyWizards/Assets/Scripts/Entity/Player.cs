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

    private float halfHeight;
    private float halfWidth;

    private bool isGrounded;
    private bool isStunned;


    private void Awake ()
    {
        resourceInventory = GetComponent<ResourceInventory>();
        rigidbody = GetComponent<Rigidbody>();

        Collider col = GetComponent<Collider>();
        halfWidth = col.bounds.extents.x;
        halfHeight = col.bounds.extents.y;
    }

    private void OnEnable ()
    {
        SetStaff(staff);
    }

    private void Update ()
    {
        if(isStunned) return;
        CheckGrounded();
        Attack();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Pickup>())
            col.GetComponent<Pickup>().Collect(resourceInventory);
    }

    private void Attack ()
    {
        if (Input.GetButtonDown("Fire1"))
            staff.Attack();
    }

    private void CheckGrounded ()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if(Physics.SphereCast(ray, 0.5f, halfHeight))
        {
            if (!isGrounded)
                OnLand.Invoke();

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void CastSpell ()
    {
        staff.CastSpell();
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
