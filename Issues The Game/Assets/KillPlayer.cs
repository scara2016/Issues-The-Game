using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    PlayerHealth playerHealth;


    private void OnCollisionEnter2D(Collision2D col)
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.health = 0;
    }

}
