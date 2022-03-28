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
    public float decceleration = 7f;
    public float velPower = 0.9f;
    public float frictionAmount = 0.1f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool jumpCooldownStart = false;
    public float jumpCooldown = 5f;
    private float jumpCooldownTimer = 0f;
    public float jumpVelocity = 1f;

    private AnimationController controller;

    //Animation States - Name in quotes should equal state name in Animator
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_FALL = "Fall";

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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        float moveInput = playerControls.Main.Move.ReadValue<float>();

        float jumpInput = playerControls.Main.Jump.ReadValue<float>();
        Debug.Log(IsGrounded());
        targetSpeed = moveInput * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
        #endregion

        #region AnimateMovement
        // Reads Input Value to change state
        if (IsGrounded())
        {
            if (moveInput != 0) //When input is given
            {
                controller.ChangeAnimationState(PLAYER_RUN);
            }
            else //When no input is given
            {
                controller.ChangeAnimationState(PLAYER_IDLE);
            }
        }

        if (moveInput > 0) //When running to the right
        {
            rb.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0) //When running to the left
        {
            rb.transform.localScale = new Vector3(-1, 1, 1);
        }

        #endregion

        #region Friction
        if (Mathf.Abs(moveInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);

        }
        #endregion
        if (jumpCooldownStart)
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= jumpCooldown)
            {
                jumpCooldownStart = false;
                jumpCooldownTimer = 0f;
            }
        }
        if (jumpInput != 0 && IsGrounded() && !jumpCooldownStart)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpCooldownStart = true;
            Debug.Log("Called");
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && jumpInput == 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }


    }

    private bool IsGrounded()
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

            if (rb.velocity.y > 0)
            {
                controller.ChangeAnimationState(PLAYER_JUMP); //Plays Jump State
            }

            if (rb.velocity.y < 0)
            {
                controller.ChangeAnimationState(PLAYER_FALL); //Plays Fall State when velocity is less than 0.
            }
        }
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(0, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.extents.x), rayColor);

        return raycastHit.collider != null;
    }

    public float GetSpeed()
    {
        return targetSpeed; //Public Getter to obtain speed while keeping targetSpeed private
    }
}
