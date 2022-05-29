using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Movement move;
    private string currentState;
    public AnimationClip victoryDanceClip;
    private AnimationEvent evt1;
    private LevelTransition lvlTransition;

    // private WeaponPickup wp;

    // Sprite color
    // private SpriteRenderer sprite;

    void Awake()
    {
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
        animator = GetComponent<Animator>();
        move = GetComponent<Movement>();

        evt1 = new AnimationEvent();
        evt1.time = 6f;
        evt1.functionName = "WinProcess";
        // Adds animation event to GoalDance clip which MUST be public.
        // Should add an event that disables the player controls, then start the dance animation. 

        victoryDanceClip.AddEvent(evt1);
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

    public void DashState()
    {
        animator.SetTrigger("Dash");
    }

    public void GoalState()
    {
        move.DisableControls();
        animator.SetTrigger("Win");
    }

    private void WinProcess()
    {
        lvlTransition.LoadScene();
    }
}
