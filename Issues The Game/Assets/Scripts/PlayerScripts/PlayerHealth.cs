using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    public float health;
    private Rigidbody2D rb;

    // private PlayerControls playerControls;

    // private Movement movement;

    // private float moveInput;

    [HideInInspector]
    public bool isDead;
    // [HideInInspector]
    public bool isTakingDamage;

    public bool hit;

    [SerializeField]
    private float verticalKnockbackForce;
    [SerializeField]
    private float horizontalKnockbackForce;

    [SerializeField]
    private float invulnerabilityTime;

    [SerializeField]
    private float cancelMovementTime;

    [HideInInspector]
    public Enemy enemy;
    private AnimationController controller;

    // void OnEnable() {
    //     playerControls.Enable();
    // }

    // void OnDisable() {
    //     playerControls.Disable();
    // }
    void Start()
    {
        health = maxHealth;
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        controller = GetComponent<AnimationController>();
        // movement = this.GetComponent<Movement>();
        // playerControls = new PlayerControls();
    }

    public void InkDamage(float inkDamage)
    {
        health -= inkDamage*Time.deltaTime;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if(!hit)
        {
            hit = true;
            health -= damage;
            if (health <= 0)
            {
                isDead = true;
                GetComponent<Collider2D>().enabled = false;
                this.enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // hit bool is set to true, changed to false after knockback
        if(hit)
        {
            HandleKnockBack();
        }
    }

    private void HandleKnockBack()
    {
        isTakingDamage = true;
        rb.AddForce(Vector2.up * verticalKnockbackForce);
        
        if(transform.position.x < enemy.transform.position.x)
        {
            rb.AddForce(Vector2.left * horizontalKnockbackForce);
            controller.HurtState(true); //Plays damage animation
        }
        else 
        {
            rb.AddForce(Vector2.right * horizontalKnockbackForce);
            controller.HurtState(true); //Plays damage animation
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
            isTakingDamage = false;
        }
    }


    // private void StopMovement()
    // {
    //     moveInput = playerControls.Main.Move.ReadValue<float>();
    //     // Debug.Log(cancelMovementTime);
    //     if(moveInput != 0 && !isTakingDamage)
    //     {
    //         movement.enabled = true;
    //     }
    //     else 
    //     {
    //         movement.enabled = false;
    //     }
    // }


}
