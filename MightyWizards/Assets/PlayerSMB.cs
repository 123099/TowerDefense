using UnityEngine;
using System.Collections;

public class PlayerSMB : StateMachineBehaviour {

    private Player player;
    private Animator animator;

    public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;

        player = FindObjectOfType<Player>();

        player.OnMove.AddListener(() => OnPlayerMove());
        player.OnStop.AddListener(() => OnPlayerStop());

        player.OnJumpUp.AddListener(() => OnPlayerJumpUp());
        player.OnJumpDown.AddListener(() => OnPlayerJumpDown());
        player.OnLand.AddListener(() => OnPlayerLand());

        player.OnAttack.AddListener(() => OnPlayerAttack());
        player.OnSpellCastStart.AddListener(() => OnPlayerStartSpellCast());
    }

    private void OnPlayerMove ()
    {
        animator.SetFloat("Speed", 1);   
    }

    private void OnPlayerStop ()
    {
        animator.SetFloat("Speed", 0);
    }

    private void OnPlayerJumpDown ()
    {
        animator.SetTrigger("Jump Down");
    }

    private void OnPlayerJumpUp ()
    {
        animator.SetBool("Jumping", true);
    }

    private void OnPlayerLand ()
    {
        animator.SetBool("Jumping", false);
    }

    private void OnPlayerAttack ()
    {
        animator.SetBool("Attacking", true);
    }

    private void OnPlayerStartSpellCast ()
    {
        animator.SetTrigger("Special Attack");
    }
}
