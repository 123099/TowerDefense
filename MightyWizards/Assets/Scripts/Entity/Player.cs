﻿using UnityEngine;
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

    public RateTimer attackRate;

    public UnityEvent OnLand;
    public UnityEvent OnJump;

    private ResourceInventory resourceInventory;
    private Rigidbody rigidbody;
    private Collider collider;

    private float halfHeight;

    private bool isStunned;
    private bool isGrounded;

    private Collider ground;

    private GameObject equippedStaffClone;

    private Vector3 previousVelocity;

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

        previousVelocity = rigidbody.velocity;

        CheckGrounded();
        Attack();
        BuildWall();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Pickup>())
            col.gameObject.GetComponent<Pickup>().Collect(resourceInventory);

        if (col.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            if (col.contacts.Length > 0 && col.contacts[0].normal.y == -1)
            {
                Physics.IgnoreCollision(collider, col.collider);
                rigidbody.velocity = previousVelocity;
            }
        }
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
        if (Input.GetButtonDown("Fire") && attackRate.IsReady())
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
        if (equippedStaffClone)
        {
            staff.spell.PassiveStop();
            Destroy(equippedStaffClone);
        }

        this.staff = staff;
        equippedStaffClone = staff.Equip(staffSpawn);

        staff.spell.PassiveStart();
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
