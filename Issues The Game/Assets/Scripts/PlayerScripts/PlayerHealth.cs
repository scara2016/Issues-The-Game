using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    public float health;
    private Rigidbody2D rb;

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

    [HideInInspector]
    public Enemy enemy;
    private AnimationController controller;
    public AnimationClip clip;
    private AnimationEvent evt1; 
    private Animator anim;
    private Movement movement;

    // void OnEnable() {
    //     playerControls.Enable();
    // }

    // void OnDisable() {
    //     playerControls.Disable();
    // }
    void Start()
    {
        movement = GetComponent<Movement>();
        health = maxHealth;
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        controller = GetComponent<AnimationController>();
        // movement = this.GetComponent<Movement>();
        // playerControls = new PlayerControls();
        evt1 = new AnimationEvent();

        //Parameters for AnimationEvent.
        //Make sure the Die Animation Clip is referenced in PlayerHealth component
        evt1.time = 1f; //Sets the avent on the last frame
        evt1.functionName = "DestroyPlayer";

        //This assigns the event to the Animation Clip
        anim = gameObject.GetComponent<Animator>();
        clip.AddEvent(evt1);
    }

    public void InkDamage(float inkDamage)
    {
        health -= inkDamage*Time.deltaTime;
        if (health <= 0)
        {
            Die();
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
                Die();
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

    private void Die()
    {
        isDead = true;
        movement.DisableControls(); //Disables controls on death
        Debug.Log("He ded tho: " + isDead);
        // GetComponent<Collider2D>().enabled = false;
        // this.enabled = false;
        controller.DieState(true);
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject); //Needs to be separate cause of animation event timing
    }

    private void HandleKnockBack()
    {
        isTakingDamage = true;
        rb.AddForce(Vector2.up * verticalKnockbackForce);
        
        if(transform.position.x < enemy.transform.position.x)
        {
            rb.AddForce(Vector2.left * horizontalKnockbackForce);
            controller.HurtState(); //Plays damage animation
        }
        else 
        {
            rb.AddForce(Vector2.right * horizontalKnockbackForce);
            controller.HurtState(); //Plays damage animation
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


}
