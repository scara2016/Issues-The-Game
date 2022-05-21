using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Collider2D[] weaponsList;
    private PlayerControls playerControls;
    private float attackInput;

    public int attackDamage = 10;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    private Movement movement;

    private float pickUpInput;

    //Weapon hit area
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;


    [SerializeField]
    private Weapon weapon;

    private WeaponPickup wpnPickup;

    private Collider2D[] hitEnemies;

    private AnimationController controller;
   


    void Awake()
    {
        controller = this.GetComponent<AnimationController>();

        playerControls = new PlayerControls();
        weaponsList = new Collider2D[1];
        movement = this.GetComponent<Movement>();
        controller = GetComponent<AnimationController>();
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void OnDisable() {
        playerControls.Disable();
    }



    // Update is called once per frame
    void Update()
    {
        if(controller == null)
        {
            Debug.Log("Null Controller");
        }
        // pickUpInput = playerControls.Main.PickUp.ReadValue<float>();
        attackInput = playerControls.Main.Attack.ReadValue<float>();
        

        if(Time.time >= nextAttackTime)
        {
            if(attackInput != 0 && Weapon.Instance.Equiped == true)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // play animation
        controller.SwingAttack();
        //Detect enemies in range 
        
        //Damage them 
        hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
