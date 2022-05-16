using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    private float moveInput;

    public GameObject weaponHolder;
    private SizzleSword sizzleSword;
    private ZapBoots zapBoots;

    private GameObject player;
    private Movement playerMovement;
    private PlayerControls playerControls;

    private PlayerCombat combat;

    private Movement movement;

    public bool pickUpAllowed;
    // public bool itemPicked;
    
    private float pickUpInput;
    private float dashInput;

    public float dashCoolDown = 2f;
    private bool dashCoolDownStart = false;
    private float dashCoolDownTimer;

    [SerializeField]
    private Weapon weapon;

    

    private SpriteRenderer sprite;


    void Start () {
        pickUpAllowed = false;
    }

    void Awake (){
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Movement>();
        zapBoots = GameObject.FindGameObjectWithTag("Boots").GetComponent<ZapBoots>();
        sizzleSword = GameObject.FindGameObjectWithTag("Sword").GetComponent<SizzleSword>();
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
        dashInput = playerControls.Main.Dash.ReadValue<float>();

        if (dashCoolDownStart)
        {
            dashCoolDownTimer += Time.deltaTime;
        }
        if(dashCoolDownStart && dashCoolDownTimer >= dashCoolDown)
        {
            dashCoolDownStart = false;
            dashCoolDownTimer = 0f;
        }
        if (dashInput != 0 && !dashCoolDownStart)
        {
            dashCoolDownStart = true;
            playerMovement.Dash();
        }

        if (pickUpAllowed && pickUpInput != 0) {
            PickUp();
            
            if(transform.parent.CompareTag(weaponHolder.tag))
            {
                if(this.CompareTag("Sword")) 
                {
                    combat.attackDamage = 20;
                    pickUpAllowed = false;
                    Weapon.Instance.GetWeapon();
                        if(weaponHolder.GetComponentInChildren<ZapBoots>() != null)
                        {
                            Debug.Log("Reached");
                            Destroy(zapBoots.gameObject);
                        }
                }
                if (this.CompareTag("Boots")) 
                {
                    combat.attackDamage = 5;
                    pickUpAllowed = false;
                    Weapon.Instance.GetWeapon();
                   
                        if(weaponHolder.GetComponentInChildren<SizzleSword>() != null)
                        {
                            Debug.Log("Reached");
                            Destroy(sizzleSword.gameObject);
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
        gameObject.transform.parent = weaponHolder.transform;
        GetComponent<Collider2D>().enabled = false;
        this.sprite.enabled = false;

        Debug.Log("PickedUp");

    }
}
