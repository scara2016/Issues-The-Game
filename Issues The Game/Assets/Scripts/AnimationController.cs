using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private Movement movement;
    private Rigidbody2D rb;

    void Awake()
    {
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // For Animator
        animator.SetFloat("Speed", Mathf.Abs(movement.GetSpeed())); //When speed is greater than greater than in state's settings, triggers animation
        if (movement.GetSpeed() > 0)
        {
            rb.transform.localScale = new Vector3(1, 1, 1); //Hard code
        }
        else if (movement.GetSpeed() < 0)
        {
            rb.transform.localScale = new Vector3(-1, 1, 1); //Hard Code
        }
        // When speed is greater than 0, stickman faces right. Else, faces left.
    }

    public void isJumping(bool jump)
    {
        animator.SetBool("isJumping", jump);
        /* When called, sets the bool parameter in the state
        to true or false. Called within isGrounded in Movement.cs
        */
        
        if (jump == true)
        {
            Debug.Log("Jumped"); //Debugging
        }
    }
}
