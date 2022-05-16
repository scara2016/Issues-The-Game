using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAIPatrol : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private LayerMask platformLayerMask;

    Rigidbody2D myRigidbody;
    BoxCollider2D boxCollider;

    public float flipCooldown = 1f;
    public float rushSpeed = 20f;
    private float flipTimer;
    private bool flipTimerStart = false;
    bool right = true;
    private EnemyDetectionCircle detectionCircle;
    private Movement player;

    public float 

    void Start()
    {
        detectionCircle = GetComponent<EnemyDetectionCircle>();
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!detectionCircle.PlayerSeen)
        {
            NormalMovement();
        }
        else
        {
            RushMovement();
        }
    }

    private void RushMovement()
    {
        myRigidbody.AddForce(new Vector2(((player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x)) * rushSpeed, 0));
    }

    private void NormalMovement()
    {
        if (flipTimerStart)
        {
            flipTimer += Time.deltaTime;
        }
        if (flipTimer >= flipCooldown)
        {
            flipTimer = 0f;
            flipTimerStart = false;
        }
        if (right)
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
        if (!IsGrounded() && !flipTimerStart)
        {
            flipTimerStart = true;
            if (right)
            {
                right = false;
            }
            else
            {
                right = true;
            }
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.3f;
        Color rayColor;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size * 2f, 0f, Vector2.down, extraHeight, platformLayerMask);
        if (raycastHit.collider != null) //When grounded
        {
            rayColor = Color.green;
        }
        else //When not grounded
        {
            rayColor = Color.red;
        }

        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (right)
        {
            right = false;
        }
        else
        {
            right = true;
        }
    }


}
