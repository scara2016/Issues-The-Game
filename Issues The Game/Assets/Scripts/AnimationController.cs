using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Movement move;
    private string currentState;

    //Animation States - Name in quotes should equal state name in Animator
    const string PLAYER_IDLE = "Idle";
    //const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_FALL = "Fall";
    const string PLAYER_WALLSLIDE = "WallSlide";

    void Awake()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<Movement>();
    }

    private void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState)
        {
            return;
        }

        //simply plays the animation
        animator.Play(newState);

        //reassigns the current state
        currentState = newState;
    }

    public void RunState(bool run)
    {
        animator.SetBool("isRunning", run);
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

}
