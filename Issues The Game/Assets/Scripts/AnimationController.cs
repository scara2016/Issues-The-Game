using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Movement move;
    private string currentState;

    // private WeaponPickup wp;

    // Sprite color
    // private SpriteRenderer sprite;

    void Awake()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<Movement>();
    }

    public void PlayState(string stateName)
    {
        animator.Play(stateName);
    }

    public void RunState(bool run)
    {
        animator.SetBool("isRunning", run);
    }

    public void WalkState(bool walk)
    {
        animator.SetBool("isWalking", walk);
    }

    public void JumpState(bool jump)
    {
        animator.SetBool("isJumping", jump);
    }

    public void AirState(bool fall)
    {
        animator.SetBool("isMidAir", fall);
    }

    public void WallSlideState(bool wallslide)
    {
        animator.SetBool("isWallSliding", wallslide);
    }

    public void SwingAttack()
    {
        if (move.IsGrounded())
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
        }
        if (move.IsGrounded() == false)
        {
            animator.ResetTrigger("AirAttack");
            animator.SetTrigger("AirAttack");
        }
    }

    public void CrouchState(bool crouch)
    {
        animator.SetBool("isCrouching", crouch);
    }

    public void HurtState()
    {
        animator.ResetTrigger("Ow");
        animator.SetTrigger("Ow");
    }

    public void DieState()
    {
        animator.SetTrigger("Dead");
    }

}
