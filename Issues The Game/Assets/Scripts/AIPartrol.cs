using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPartrol : MonoBehaviour
{
  
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private LayerMask platformLayerMask;

    Rigidbody2D myRigidbody;
    BoxCollider2D boxCollider;

    public float flipCooldown=1f;
    private float flipTimer;
    private bool flipTimerStart = false;
    bool right = true;
    void Start()
    {
        
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
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
        if (!IsGrounded()&&!flipTimerStart)
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCollider.bounds.size*2f, 0f, Vector2.down, extraHeight, platformLayerMask);
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
