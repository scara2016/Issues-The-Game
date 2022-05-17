using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionCircle : MonoBehaviour
{
    BoxCollider2D collider;
    public float visionRadius;
    private bool playerSeen = false;
    public bool PlayerSeen
    {
        get
        {
            return playerSeen;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetected();
    }

    private bool PlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(collider.bounds.center, visionRadius);
        playerSeen = false;
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                playerSeen = true;
            }
        }
        

        return playerSeen;
    }

}
