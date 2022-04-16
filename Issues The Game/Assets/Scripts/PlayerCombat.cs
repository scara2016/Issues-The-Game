using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControls playerControls;
    private float attackInput;

    public int attackDamage = 10;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    // private Movement movement;

    //Weapon hit area
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    [SerializeField]
    private Weapon weapon;

    private Collider2D[] hitEnemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        playerControls = new PlayerControls();
        weapon = GetComponent<Weapon>();
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

        if(Time.time >= nextAttackTime)
        {
            if(attackInput != 0 && weapon.Equiped == true)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // play animation
        //Detect enemies in range 
        hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayer);
        //Damage them 
        foreach(Collider2D enemy in hitEnemies)
        {
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
