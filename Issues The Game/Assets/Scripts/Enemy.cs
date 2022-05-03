using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float harm = 40;

    // [SerializeField] private LayerMask platformLayerMask;

    // private BoxCollider2D boxCollider;

    private PlayerHealth player;
    // private Player knockback;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public bool IsGrounded()
    // {
    //     float extraHeight = 0.1f;
    //     Color rayColor;
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
    //     if (raycastHit.collider != null) //When grounded
    //     {
    //         rayColor = Color.green;
    //     }
    //     else //When not grounded
    //     {
    //         rayColor = Color.red;
    //     }
    //     Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
    //     Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
    //     Debug.DrawRay(boxCollider.bounds.center + new Vector3(0, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.extents.x), rayColor);

    //     return raycastHit.collider != null;
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        // player = other.gameObject.GetComponent<PlayerHealth>();
        // player = other;
        if(other.CompareTag("Player"))
        {
            player.TakeDamage(harm);

            // StartCoroutine(player.Knockback(0.025f, 350, player.transform.position));
        }
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.CompareTag("Player"))
    //     {
    //         player.hit = false;
    //     }
    // }
}
