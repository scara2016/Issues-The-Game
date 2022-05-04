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

    private AnimationController controller;
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



    // Update is called once per frame
    void Update()
    {
        attackInput = playerControls.Main.Attack.ReadValue<float>();
        Debug.Log(attackDamage);

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
