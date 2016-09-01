using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour {

    [SerializeField] private Staff staff;
    [SerializeField] private Transform staffSpawn;
    [SerializeField] private float jumpForce;

    private Collider collider;
    private Rigidbody rigidbody;
    private Animator anim;

    private float halfHeight;

    private void Awake ()
    {
        anim = GetComponent<Animator>();
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
        Move();
        Jump();
        Attack();
    }

    private void Attack ()
    {
        if (Input.GetButtonDown("Fire1"))
            staff.Attack();
    }

    private void Move ()
    {
        float input = Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(0, 90f * input, 0);
        anim.SetFloat("Speed", Mathf.Abs(input));
    }

    private void Jump ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Ray ray = new Ray(transform.position, -transform.up);
            if (Physics.SphereCast(ray, 0.5f, halfHeight + 0.05f))
            {
                rigidbody.AddRelativeForce(Vector3.up * jumpForce);
                //anim.SetTrigger("Jump");
            }
        }
    }

    public void SetStaff(Staff staff)
    {
        this.staff = staff;
        staff.Equip(staffSpawn);
    }
}
