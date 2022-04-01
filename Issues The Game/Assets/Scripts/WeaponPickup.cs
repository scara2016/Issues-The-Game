using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    
    public GameObject[] weapons;
    public GameObject weaponHere;
    private PlayerControls playerControls;

    private bool pickUpAllowed;
    
    private float pickUpInput;


    void Start () {
        // weaponHere = weapons [Random.Range (0, weapons.Length)];
        // GetComponent<SpriteRenderer> ().sprite = weaponHere.GetComponent<SpriteRenderer> ().sprite;
    }

    void Awake (){
        playerControls = new PlayerControls();
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
    
    // PickUp destroys object
    void PickUp()
    {
        // Equip weapon to player

        Destroy(gameObject);
    }
}
