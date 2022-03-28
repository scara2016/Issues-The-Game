using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
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
}
