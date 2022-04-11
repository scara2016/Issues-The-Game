using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControls playerControls;
    private float attackInput;

    // private Movement movement;

    //Weapon hit area
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    [SerializeField]
    private Weapon weapon;
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



        if(attackInput != 0 && weapon.Equiped == true)
        {
           Attack();
        }
    }

    void Attack()
    {
        // play animation
        //Detect enemies in range 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayer);
        //Damage them 
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
