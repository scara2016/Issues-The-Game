using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAIPatrol : MonoBehaviour
{
    enum AIState
    {
        Moving,
        SpottedPlayer,
        Attacking,
        Reloading,
        LostPlayer
    }

    private AIState aiState;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private LayerMask platformLayerMask;

    Rigidbody2D myRigidbody;
    BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public float noticeTimer;
    public float reloadTimer;
    public float lostTimer;
    public float bulletLobHeight;
    

    public float flipCooldown = 1f;
    private float flipTimer;
    private bool flipTimerStart = false;
    bool right = true;
    private EnemyDetectionCircle detectionCircle;
    private Movement player;
    private EnemyAnimationController controller;

    private void Start()
    {
        detectionCircle = GetComponent<EnemyDetectionCircle>();
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<EnemyAnimationController>(); //Separate Animation Controller than player's
        aiState = AIState.Moving;
    }
    private float noticeT;
    private float reloadT;
    private float lostT;
    
    // Update is called once per frame
    void Update()
    {

        switch (aiState)
        {
            case AIState.Moving:
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
                if (detectionCircle.PlayerSeen)
                {
                    aiState = AIState.SpottedPlayer;
                }
                
                controller.ReloadState(false);
                controller.MoveState(true);
                break;
            case AIState.SpottedPlayer:

                noticeT += Time.deltaTime;
                if (noticeT >= noticeTimer)
                {
                    noticeT = 0;
                    aiState = AIState.Attacking;
                    
                }
                if (!detectionCircle.PlayerSeen)
                {
                    noticeT = 0;
                    aiState = AIState.Moving;
                }
                controller.MoveState(false);
                controller.ReloadState(true);
                break;
            case AIState.Attacking:
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = transform.position;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                LaunchBullet(CalculateLaunchVelocity(bullet), bulletRB);
                aiState = AIState.Reloading;

                controller.AtkState();
                controller.ReloadState(false);
                controller.MoveState(false);
                
                break;
            case AIState.Reloading:
                reloadT += Time.deltaTime;
                if (reloadT >= reloadTimer)
                {
                    reloadT = 0;
                    aiState = AIState.Attacking;
                }
                if (!detectionCircle.PlayerSeen)
                {
                    reloadT = 0;
                    aiState = AIState.LostPlayer;
                }

                controller.ReloadState(true);
                break;

            case AIState.LostPlayer:

                lostT += Time.deltaTime;
                if (lostT >= lostTimer)
                {
                    lostT = 0;
                    aiState = AIState.Moving;
                    controller.MoveState(true);
                }
                if (detectionCircle.PlayerSeen)
                {
                    lostT = 0;
                    aiState = AIState.Attacking;
                }
               
                controller.MoveState(false);
                break;
        }
    }

    private Vector2 CalculateLaunchVelocity(GameObject bullet)
    {
        float yDisplacement = player.transform.position.y - bullet.transform.position.y;
        float xDisplacement = player.transform.position.x - bullet.transform.position.x;
        Vector2 yVelocity = Vector2.up * Mathf.Sqrt(-2 * Physics2D.gravity.y * bulletLobHeight);
        Vector2 xVelocity = new Vector2(xDisplacement / (Mathf.Sqrt(-2 * bulletLobHeight / Physics2D.gravity.y) + Mathf.Sqrt(2 * (yDisplacement - bulletLobHeight) / Physics2D.gravity.y)),0);
        return xVelocity + yVelocity;

    }

    private void LaunchBullet(Vector2 launchVelocity, Rigidbody2D bulletRB)
    {
        bulletRB.velocity = launchVelocity;
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
