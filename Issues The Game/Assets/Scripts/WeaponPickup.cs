using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    

    public GameObject weaponHolder;
    private GameObject sizzleSword;
    private GameObject zapBoots;

    private GameObject player;
    private PlayerControls playerControls;

    private PlayerCombat combat;

    private Movement movement;

    public bool pickUpAllowed;
    // public bool itemPicked;
    
    private float pickUpInput;
    

    [SerializeField]
    private Weapon weapon;

    

    private SpriteRenderer sprite;


    void Start () {
        pickUpAllowed = false;
    }

    void Awake (){
        player = GameObject.FindGameObjectWithTag("Player");
        zapBoots = GameObject.FindGameObjectWithTag("Boots");
        sizzleSword = GameObject.FindGameObjectWithTag("Sword");
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        playerControls = new PlayerControls();
        combat = player.GetComponent<PlayerCombat>();
        movement = player.GetComponent<Movement>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void OnDisable() {
        playerControls.Disable();
    }

    void Update (){
        pickUpInput = playerControls.Main.PickUp.ReadValue<float>();
        Debug.Log(pickUpInput);
        Debug.Log(combat.attackDamage);
        Debug.Log(movement.moveSpeed);
        
        if(pickUpAllowed && pickUpInput != 0) {
            PickUp();
            
            if(this.transform.parent.CompareTag(weaponHolder.tag))
            {
                if(this.CompareTag("Sword")) 
                {
                    combat.attackDamage = 20;
                    movement.moveSpeed = 6f;
                    Debug.Log("We have " + this.name);
                    pickUpAllowed = false;
                    Weapon.Instance.GetWeapon();
                    Debug.Log(weaponHolder.transform.Find("Zap Boots"));
                        if(weaponHolder.transform.Find("Zap Boots"))
                        {
                            Destroy(zapBoots);
                        }
                }
                else if (this.CompareTag("Boots")) 
                {
                    combat.attackDamage = 5;
                    movement.moveSpeed = 15f;
                    Debug.Log("We have " + this.name);
                    pickUpAllowed = false;
                    Weapon.Instance.GetWeapon();
                    Debug.Log(weaponHolder.transform.Find("Sizzle Sword"));
                        if(weaponHolder.transform.Find("Sizzle Sword"))
                        {
                            Destroy(sizzleSword);
                        }
                }
            }
            // pickUpAllowed = false;
            // Weapon.Instance.GetWeapon();
        }
    }
    

    // When player touches object 
    // Pickup activates
    void OnTriggerEnter2D(Collider2D other)
    {
        pickUpAllowed = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            pickUpAllowed = false;
        }
    }
    
    // PickUp destroys object
    public void PickUp()
    {

        // makes item dissapear after pickup
        // Destroy(gameObject);
        gameObject.transform.parent = weaponHolder.transform;
        GetComponent<Collider2D>().enabled = false;
        this.sprite.enabled = false;

        Debug.Log("PickedUp");

    }
}
