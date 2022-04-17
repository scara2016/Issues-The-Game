using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    // public Weapon[] weaponsList;
    

    public GameObject weaponHolder;

    // List<GameObject> allWeapons = new List<GameObject>();
    // List<GameObject> weapons = new List<GameObject>();
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
        // itemPicked = false;
        // weaponHere = weapons [Random.Range (0, weapons.Length)];
        // GetComponent<SpriteRenderer> ().sprite = weaponHere.GetComponent<SpriteRenderer> ().sprite;
    }

    void Awake (){
        playerControls = new PlayerControls();
        // weaponsList = new Weapon[1];
        combat = GetComponent<PlayerCombat>();
        movement = GetComponent<Movement>();
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
        
        if(pickUpAllowed && pickUpInput != 0) {
            PickUp();
            pickUpAllowed = false;
            Weapon.Instance.GetWeapon();
        }
    }
    

    // When player touches object 
    // Pickup activates
    void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.CompareTag("Player"))
        // {
        //     combat.weaponsList[0] = Weapon.Instance;
        //     Debug.Log("We have " + Weapon.Instance.name);
            pickUpAllowed = true;
        //     if(gameObject.CompareTag("Sword"))
        //     {
        //         Debug.Log("We have " + Weapon.Instance.name);
        //         combat.attackDamage = 20;
        //     } else if(gameObject.CompareTag("Boots"))
        //     {
        //         Debug.Log("We have " + Weapon.Instance.name);
        //         combat.attackDamage = 5;
        //         movement.moveSpeed = 15f;
        //     }
        // }

        // foreach(Collider2D wpn in weaponsList)
        // {
        //     Debug.Log(wpn.name);
        // }
        // float pickUpInput = playerControls.Main.PickUp.ReadValue<float>();
        // if(other.CompareTag("Player"))
        // {
            
        // }
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
        // sprite = player.GetComponent<SpriteRenderer>();
        // Equip weapon to player
        // sprite.color = new Color(1,0,0,1);

        // store weapon in an array
        // weapons.Add(weapon);
        // allWeapons.Add(weapon);

        // makes item dissapear after pickup
        gameObject.transform.parent = weaponHolder.transform;
        // Destroy(gameObject);
        GetComponent<Collider2D>().enabled = false;
        this.sprite.enabled = false;
        // statement to say that an item has been picked up
        // itemPicked = true;
        Debug.Log("PickedUp");

    }
}
