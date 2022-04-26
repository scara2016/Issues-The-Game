using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask platformLayerMask;
    private PlayerControls playerControls;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    public float acceleration = 7f;
    public float moveSpeed = 10f;
    private float targetSpeed;
    private float moveInput;
    private float jumpInput;
    private float crouchInput;
    public float decceleration = 7f;
    public float velPower = 0.9f;
    public float frictionAmount = 0.1f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool jumpCooldownStart = false;
    public float jumpCooldown = 5f;
    private float jumpCooldownTimer = 0f;
    public float jumpVelocity = 1f;

    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.5f;
    private float slideTimer = 0f;
    public float slideCooldown = 0.5f;
    private bool slideCooldownStart = false;
    private bool isWallSliding = false;
    RaycastHit2D wallCheckHitLeft;
    RaycastHit2D wallCheckHitRight;

    private AnimationController controller;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        controller = GetComponent<AnimationController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

 

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AnimateMovement();
        Friction();
        Jump();
        WallJump();
        Crouch();
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        Color rayColor;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        if (raycastHit.collider != null) //When grounded
        {
            rayColor = Color.green;
        }
        else //When not grounded
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(0, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.extents.x), rayColor);

        return raycastHit.collider != null;
    }

    private void PlayerMovement()
    {
        moveInput = playerControls.Main.Move.ReadValue<float>(); // Reads and stores movement input from inputManager
        
        jumpInput = playerControls.Main.Jump.ReadValue<float>(); // Reads and stores movement input from inputManager
        float targetSpeed = moveInput * moveSpeed; // when the player wants to move then the target speed is 1*movespeed and when they want to stop it is 0*moveSpeed
        float speedDif = targetSpeed - rb.velocity.x; //finds difference between current velocity and target velocity
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration; // calculates if accel needs to be applied positive or negative
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }

    private void AnimateMovement()
    {
        // Reads Input Value to change state
        if (IsGrounded() && !isWallSliding)
        {
            controller.WallSlideState(false);
            if (moveInput != 0)
            {
                controller.RunState(true);
            }
            else
            {
                controller.RunState(false);
                controller.WalkState(false);
            }
        }

        if (!IsGrounded() && !isWallSliding)
        {
            if (rb.velocity.y > 0 || jumpInput != 0)
            {
                controller.JumpState(true);
                controller.WallSlideState(false);
            }

            if (rb.velocity.y < 0)
            {
                controller.JumpState(false);
                controller.AirState(true); 
                controller.WallSlideState(false);
            }
        }
        else
        {
            controller.AirState(false);
            controller.JumpState(false);
        }

        if (!IsGrounded() && isWallSliding)
        {
            controller.WallSlideState(true);
        }

        if (moveInput > 0) //When running to the right
        {
            rb.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0) //When running to the left
        {
            rb.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Friction()
    {
        if (Mathf.Abs(moveInput) < 0.01f) //custom friction as we need engine friction to be 0 for wall slides
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        if (jumpCooldownStart) // so the player cannot jump in rapid succsesion
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= jumpCooldown)
            {
                jumpCooldownStart = false;
                jumpCooldownTimer = 0f;
            }
        }
        if(jumpInput != 0 && IsGrounded() && !jumpCooldownStart || (isWallSliding && jumpInput!=0 && !jumpCooldownStart)) //true if player is going to jump
        {
            rb.AddForce(Vector2.up* jumpVelocity, ForceMode2D.Impulse);
            jumpCooldownStart = true; //cooldown has started
        }
        if (rb.velocity.y < 0) // if the player has started to fall then we apply the fall multiplier
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime ;
        } 
        else if(rb.velocity.y>0 && jumpInput == 0) // if the player hasd let go early of jump button then we increase
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; 
        }
    }

    private void WallJump()
    {
        wallCheckHitRight = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, platformLayerMask);
        wallCheckHitLeft = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, platformLayerMask);

        Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
        Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);
        if (slideCooldownStart) // cooldown timer
        {
            slideTimer += Time.deltaTime;
        }
      
        if ((wallCheckHitLeft || wallCheckHitRight) && !IsGrounded() && moveInput!=0 && !isWallSliding) // if either ray is triggered and the player is set to start sliding
        {
            slideCooldownStart = true;
            isWallSliding = true;
            jumpCooldownTimer = float.MaxValue;
        }
        else if (slideTimer > slideCooldown) //ends the slide
        {
            slideTimer = 0;
            slideCooldownStart = false;
            isWallSliding = false;
        }
        if (isWallSliding) // movement condition for sliding
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            Debug.Log("Wall Sliding: " + isWallSliding); 
        }
    }

    // void SwordAttackDOne() 
    // {
    //     if(GetSpeed() > 0)
    //     {
    //         swordRight.SetActive(false);
    //     }
    //     //when facing left
    //     else
    //     {
    //         swordLeft.SetActive(false);
    //     }
    // }

    private void Crouch()
    {
        crouchInput = playerControls.Main.Crouch.ReadValue<float>();

        if (crouchInput >= 0.5 && IsGrounded() && moveInput == 0)
        {
            Debug.Log("Crouching");
            boxCollider.size = new Vector2(1, 0.7f);
            boxCollider.offset = new Vector2(0, -0.2f);
        }
        else
        {
            boxCollider.size = new Vector2(1, 1);
            boxCollider.offset = new Vector2(0, 0);
        }
    }
}
