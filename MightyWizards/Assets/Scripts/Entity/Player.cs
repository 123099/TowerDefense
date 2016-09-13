using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(ResourceInventory))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour {
    //Invulnerability - ignore collisions with the enemy and enemy projectile layer for specific time
    [Tooltip("The staff this player has equipped")]
    [SerializeField] private Staff staff;
    [Tooltip("The position and rotation at which staffs spawn")]
    [SerializeField] private Transform staffSpawn;
    [Tooltip("The speed with which the player moves left and right")]
    [SerializeField] private float moveSpeed;
    [Tooltip("The upwards speed to set once the player starts jumping")]
    [SerializeField] private float jumpSpeed;
    [Tooltip("The maximum velocity the player can move at")]
    [SerializeField] private float maxSpeed;

    #region Events
    public UnityEvent OnMove;
    public UnityEvent OnStop;
    public UnityEvent OnJumpUp;
    public UnityEvent OnJumpDown;
    public UnityEvent OnLand;
    public UnityEvent OnAttack;
    public UnityEvent OnSpellCastStart;
    #endregion

    private Transform fireLocation;

    private ResourceInventory resourceInventory;

    private Rigidbody rigidbody;
    private Collider collider;
    private Collider ground;

    private float halfHeight;

    private bool isStunned;
    private bool isGrounded;

    private GameObject equippedStaffClone;

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
        staff.spell.UpdatePassive();

        if(isStunned) return;
        if(GameUtils.IsGamePaused()) return;

        CheckGrounded();
        BuildWall();

        Move();
        Jump();
        CheckAttack();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Pickup>())
            col.gameObject.GetComponent<Pickup>().Collect(resourceInventory);
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

    private void FixedUpdate ()
    {
        if (rigidbody.velocity.magnitude > maxSpeed)
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
    }

    private void Move ()
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(input) == 1)
        {
            transform.rotation = Quaternion.Euler(0, -90f * input, 0);
            Vector3 rigidbodyVel = rigidbody.velocity;
            rigidbodyVel.x = ( transform.forward * moveSpeed ).x;
            rigidbody.velocity = rigidbodyVel;

            OnMove.Invoke();
        }
        else if (input == 0)
            OnStop.Invoke();
    }

    private void Jump ()
    {
        float input = Input.GetAxisRaw("Vertical") * Time.timeScale;

        if (IsGrounded())
        {
            if (input == 1)
            {
                Vector3 rigidbodyVel = rigidbody.velocity;
                rigidbodyVel.y = jumpSpeed;
                rigidbody.velocity = rigidbodyVel;

                OnJumpUp.Invoke();
            }
            else if (input == -1)
            {
                if (GetGround().gameObject.layer == LayerMask.NameToLayer("Platform"))
                {
                    Physics.IgnoreCollision(GetComponent<Collider>(), GetGround(), true);
                    OnJumpDown.Invoke();
                }
            }
        }
    }

    private void CheckAttack ()
    {
        if (Input.GetButtonDown("Fire"))
            OnAttack.Invoke();
    }

    public void Attack ()
    {
        staff.Attack(fireLocation);
    }

    private void CheckGrounded ()
    {
        RaycastHit hit;
        Ray ray = new Ray(collider.bounds.center, -transform.up);
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

    public void StartSpellCast ()
    {
        OnSpellCastStart.Invoke();
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
        if (equippedStaffClone)
        {
            staff.spell.PassiveStop();
            Destroy(equippedStaffClone);
        }

        this.staff = staff;
        equippedStaffClone = staff.Equip(staffSpawn);

        staff.spell.PassiveStart();

        fireLocation = equippedStaffClone.transform.Find("FireLocation");
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

    public void OnModelSet(GameObject model)
    {
        staffSpawn = GameUtils.FindRecursively(model.transform, "StaffSpawn");
        if (!staffSpawn)
            Debug.Log("Could not find 'StaffSpawn' in player model");
    }
}
