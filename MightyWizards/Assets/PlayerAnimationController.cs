﻿using UnityEngine;
using System.Collections;

public class PlayerAnimationController : StateMachineBehaviour {

    [Tooltip("The speed with which the player moves left and right")]
    [SerializeField] private float moveSpeed;
    [Tooltip("The upwards speed to set once the player starts jumping")]
    [SerializeField] private float jumpSpeed;

    private Player player;
    private Rigidbody rigidbody;
    private Animator animator;

    private bool attacking;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;

        player = FindObjectOfType<Player>();
        rigidbody = player.GetComponent<Rigidbody>();

        player.OnLand.AddListener(() => onPlayerLand());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        if(player.IsStunned()) return;
        if(GameUtils.IsGamePaused()) return;

        attacking = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");

        Move();
        Jump();
        Attack();
    }

    private void Move ()
    {
       // if(attacking) return;

        float input = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(input) == 1)
        {
            player.transform.rotation = Quaternion.Euler(0, -90f * input, 0);
            Vector3 rigidbodyVel = rigidbody.velocity;
            rigidbodyVel.x = (player.transform.forward * moveSpeed).x;
            rigidbody.velocity = rigidbodyVel;
        }

        animator.SetFloat("Speed", Mathf.Abs(input));
    }

    private void Jump()
    {
        float input = Input.GetAxisRaw("Vertical") * Time.timeScale;

        if (player.IsGrounded())
        {
            if (input == 1)
            {
                Vector3 rigidbodyVel = rigidbody.velocity;
                rigidbodyVel.y = jumpSpeed;
                rigidbody.velocity = rigidbodyVel;
                animator.SetBool("Jump", true);

                player.OnJumpUp.Invoke();
            }
            else if(input == -1)
            {
                if(player.GetGround().gameObject.layer == LayerMask.NameToLayer("Platform"))
                {
                    Physics.IgnoreCollision(player.GetComponent<Collider>(), player.GetGround(), true);
                }
            }
        }
    }

    private void onPlayerLand ()
    {
        animator.SetBool("Jump", false);
    }

    private void Attack ()
    {
        if (Input.GetButtonDown("Fire"))
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
