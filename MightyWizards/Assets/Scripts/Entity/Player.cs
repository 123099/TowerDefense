using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour {

    [SerializeField] private Staff staff;
    [SerializeField] private Transform staffSpawn;

    public UnityEvent OnLand;

    private float halfHeight;
    private float halfWidth;

    private bool isGrounded;

    private void Awake ()
    {
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
        CheckGrounded();
        Attack();
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

    public void SetStaff(Staff staff)
    {
        this.staff = staff;
        staff.Equip(staffSpawn);
    }
}
