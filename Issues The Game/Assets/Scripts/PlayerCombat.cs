using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControls playerControls;
    private float attackInput;

    [SerializeField]
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        playerControls = new PlayerControls();
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
        //Damage them 
    }
}
