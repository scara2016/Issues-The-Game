using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkDrops : MonoBehaviour
{
    private PlayerHealth player;
    public float harm = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player.InkDamage(harm);
        }
    }
}
