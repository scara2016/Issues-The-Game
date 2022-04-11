using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private static Weapon instance;

    public static Weapon Instance
    {
        get { return instance; }
    }
    public SpriteRenderer sprite;

    [SerializeField]
    private bool equiped;

    public bool Equiped 
    {
        get { return equiped; }
    }
    // public float swingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        instance = this;
    }

    public void GetWeapon()
    {
        equiped = true;
        // changeLook();
    }

    public void changeLook() 
    {
        sprite.color = new Color(1,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
