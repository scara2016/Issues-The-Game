using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    // public Transform weaponHolder;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        playerControls = new PlayerControls();
        // weapon = GetComponent<Weapon>();
        weaponsList = new Collider2D[1];
        // wpnPickup = GetComponent<WeaponPickup>();
        movement = this.GetComponent<Movement>();
        // wpnPickup = GetComponent<WeaponPickup>();
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void OnDisable() {
        playerControls.Disable();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // wpnPickup.GetComponent<WeaponPickup>().pickUpAllowed = true;
        // once sword is in contact, with player, sword is added in array
        // if(other.CompareTag("Sword"))
        // {
        //     weaponsList[0] = other;
        //     Debug.Log("We have " + other.name);
        //     attackDamage = 20;
        //     // once boots is in contact, with player, boots is added in array
        // } 
        // if(other.CompareTag("Boots"))
        // {
        //     weaponsList[0] = other;
        //     Debug.Log("We have " + other.name);
        //     attackDamage = 5;
        //     movement.moveSpeed = 15f;
        // }

        // if(other.transform.parent == weaponHolder)
        // {
        //     if(other.CompareTag("Sword"))
        //     {
        //         weaponsList[0] = other;
        //         Debug.Log("We have " + other.name);
        //         attackDamage = 20;
        //     } else if(other.CompareTag("Boots") && pickUpInput != 0)
        //     {
        //         weaponsList[0] = other;
        //         Debug.Log("We have " + other.name);
        //         attackDamage = 5;
        //         movement.moveSpeed = 15f;
        //     }
        // }
    }


    // Update is called once per frame
    void Update()
    {
        pickUpInput = playerControls.Main.PickUp.ReadValue<float>();
        attackInput = playerControls.Main.Attack.ReadValue<float>();
        Debug.Log(attackDamage);
        Debug.Log(movement.moveSpeed);

        if(Time.time >= nextAttackTime)
        {
            if(attackInput != 0 && Weapon.Instance.Equiped == true)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     // if(weapon.Equiped == true)
    //     // {
    //         // add weapon to array when picked up
    //         if(other.CompareTag("Sword"))
    //         {
    //             weaponsList[0] = other;
    //             Debug.Log("We have " + other.name);
    //         }

    //         if(other.CompareTag("Boots"))
    //         {
    //             weaponsList[0] = other;
    //             Debug.Log("We have " + other.name);
    //         }
    //     // }
    // }

    void Attack()
    {
        // play animation
        //Detect enemies in range 
        
        //Damage them 
        hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayer);

        // if(wpn.CompareTag("Sword"))
        // {
            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
        // }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
