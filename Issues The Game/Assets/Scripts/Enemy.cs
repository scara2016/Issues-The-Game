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

    void OnTriggerEnter2D(Collider2D other)
    {
        // player = other.gameObject.GetComponent<PlayerHealth>();
        // player = other;
        if(other.CompareTag("Player"))
        {
            player.TakeDamage(harm);

            // StartCoroutine(player.Knockback(0.025f, 100, 2000));
        }
    }
}
