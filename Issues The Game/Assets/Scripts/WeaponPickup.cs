using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    
    public GameObject[] weapons;
    public GameObject weaponHere;
    void Start () {
        weaponHere = weapons [Random.Range (0, weapons.Length)];
        GetComponent<SpriteRenderer> ().sprite = weaponHere.GetComponent<SpriteRenderer> ().sprite;

    }
}
