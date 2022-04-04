using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    
    // public GameObject[] weapons;
    // public GameObject weaponHere;
    private PlayerControls playerControls;

    public bool pickUpAllowed;
    // public bool itemPicked;
    
    private float pickUpInput;

    [SerializeField]
    private Weapon weapon;

    // private SpriteRenderer sprite;


    void Start () {
        pickUpAllowed = false;
        // itemPicked = false;
        // weaponHere = weapons [Random.Range (0, weapons.Length)];
        // GetComponent<SpriteRenderer> ().sprite = weaponHere.GetComponent<SpriteRenderer> ().sprite;
    }

    void Awake (){
        playerControls = new PlayerControls();
        // sprite = new SpriteRenderer();
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
        // float pickUpInput = playerControls.Main.PickUp.ReadValue<float>();
        if(other.CompareTag("Player"))
        {
            pickUpAllowed = true;
        }
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

        // makes item dissapear after pickup
        Destroy(gameObject);
        // statement to say that an item has been picked up
        // itemPicked = true;
        Debug.Log("PickedUp");

    }
}
