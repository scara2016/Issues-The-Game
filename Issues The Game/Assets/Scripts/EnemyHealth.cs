using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 25;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float damage)
    {
        health -= damage*Time.deltaTime;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
