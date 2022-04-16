using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        // die animation

        // disable enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
