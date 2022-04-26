using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    // public Weapon[] weaponsList;
    public GameObject[] weaponsList;
    

    public GameObject weaponHolder;
    public GameObject sizzleSword;
    public GameObject zapBoots;

    private GameObject player;

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
        player = GameObject.FindGameObjectWithTag("Player");
        playerControls = new PlayerControls();
        // weaponsList = new Weapon[1];
        weaponsList = new GameObject[1];
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
        // combat.attackDamage = 60;
        
        if(pickUpAllowed && pickUpInput != 0) {
            PickUp();

            if(sizzleSword.transform.parent.CompareTag(weaponHolder.tag) )
            {
                combat.attackDamage = 20;
                Debug.Log("We have " + this.name);
            } 
            
            else if(zapBoots.transform.parent.CompareTag(weaponHolder.tag) )
            {
                combat.attackDamage = 5;
                movement.moveSpeed = 15f;
                Debug.Log("We have " + this.name);
            }
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
        // allWeapons.Add(weapon);\

        // if(combat.weaponsList[0] == this.gameObject)
        // {
        //     Debug.Log("We have " + this.name);
        //     combat.attackDamage = 20;
        // }

        // makes item dissapear after pickup
        // Destroy(gameObject);
        gameObject.transform.parent = weaponHolder.transform;
        GetComponent<Collider2D>().enabled = false;
        this.sprite.enabled = false;

        Debug.Log("PickedUp");
        // statement to say that an item has been picked up
        // itemPicked = true;

    }
}
