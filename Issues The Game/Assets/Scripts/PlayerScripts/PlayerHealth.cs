using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    public float health;
    private Rigidbody2D rb;

    private SpriteRenderer playerSpriteRenderer;

    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool isTakingDamage;

    public bool hit;

    [SerializeField]
    public float verticalKnockbackForce;
    [SerializeField]
    public float horizontalKnockbackForce;

    [SerializeField]
    private float invulnerabilityTime;

    [SerializeField]
    private float cancelMovementTime;

    InkParticleSpawner inkParticleSpawner;

    [HideInInspector]
    public Enemy enemy;
    private AnimationController controller;
    private Movement movement;

    private MeterScript healthMeter;
    [SerializeField] AudioSource deadsfx;
    [SerializeField] AudioSource hitsfx;

    private GameManager gameManager;


    // void OnEnable() {
    //     playerControls.Enable();
    // }

    // void OnDisable() {
    //     playerControls.Disable();
    // }
    void Start()
    {
        movement = GetComponent<Movement>();
        healthMeter = FindObjectOfType<MeterScript>();
        health = maxHealth;
        healthMeter.SetMaxHealth(maxHealth);
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        controller = GetComponent<AnimationController>();
        inkParticleSpawner = GetComponentInChildren<InkParticleSpawner>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        deadsfx.Pause();
    }

    public void InkDamage(float inkDamage)
    {
        health -= inkDamage*Time.deltaTime;
        healthMeter.SetHealth(health);
        hitsfx.Play();
        if (health <= 0)
        {
            Die();
            deadsfx.UnPause();
        }
    }

    public void TakeDamage(float damage)
    {
        if(!hit)
        {
            hitsfx.Play();
            inkParticleSpawner.SpurtInk();
            hit = true;
            health -= damage;
            healthMeter.SetHealth(health);
            if (health <= 0)
            {
                Die();
                deadsfx.UnPause();
            }
        }
    }

    

    private void FixedUpdate()
    {
        deadsfx.Pause();
        // hit bool is set to true, changed to false after knockback
        if(hit)
        {
            HandleKnockBack();
        }
        if (health <= 0)
        {
            Die();
            deadsfx.UnPause();
        }
    }

    private void Die()
    {
        isDead = true;
        movement.DisableControls(); //Disables controls on death
        Debug.Log("He ded tho: " + isDead);
        // GetComponent<Collider2D>().enabled = false;
        // this.enabled = false;
        controller.DieState();
        Invoke("Respawn", 3f);
    }

    private void Respawn()
    {
        GameManager.instance.Restart();
    }
    

    private void HandleKnockBack()
    {
        isTakingDamage = true;
        rb.AddForce(Vector2.up * verticalKnockbackForce);
        if (enemy != null)
        {
            if (transform.position.x < enemy.transform.position.x)
            {
                rb.AddForce(Vector2.left * horizontalKnockbackForce);

                controller.HurtState(); //Plays damage animation
            }
            else
            {
                rb.AddForce(Vector2.right * horizontalKnockbackForce);
                controller.HurtState(); //Plays damage animation
            }
        }

        Invoke("CancelHit", invulnerabilityTime);
        Invoke("EnableMovement", cancelMovementTime);
    }

    private void CancelHit()
    {
        hit = false;
    }

    private void EnableMovement()
    {
        if (!isDead)
        {
            deadsfx.Pause();
            isTakingDamage = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Fall"))
        {
            Die();
            deadsfx.UnPause();
        }
    }


}
