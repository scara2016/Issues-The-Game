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
    private SpriteRenderer spriteRenderer;

    public float flipCooldown = 1f;
    public float rushSpeed = 20f;
    private float flipTimer;
    private bool flipTimerStart = false;
    bool right = true;
    private EnemyDetectionCircle detectionCircle;
    private Movement player;

    public float rushCoolDown = 1f;
    private float rushTimer;
    private bool rushCoolDownStart = false;

    private void Start()
    {
        detectionCircle = GetComponent<EnemyDetectionCircle>();
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rushCoolDownStart)
        {
            rushTimer += Time.deltaTime;
        }
        if (rushTimer >= rushCoolDown)
        {
            rushCoolDownStart = false;
            rushTimer = 0f;
        }
        if (!detectionCircle.PlayerSeen)
        {
            NormalMovement();
        }
        else if(!rushCoolDownStart)
        {
            RushMovement();
        }
    }

    private void RushMovement()
    {
        myRigidbody.AddForce(new Vector2(((player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x)) * rushSpeed, 0));
        spriteRenderer.color = Color.red;
        if (Vector2.Distance(transform.position, player.transform.position) <= 2f)
        {
            rushCoolDownStart = true;
            spriteRenderer.color = Color.white;
        }
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCollider.bounds.size * 2f, 0f, Vector2.down, extraHeight, platformLayerMask);
        RaycastHit2D raycastHitNew = Physics2D.Raycast(transform.position, Vector2.down, 3f, platformLayerMask);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        return raycastHitNew.collider != null;
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
