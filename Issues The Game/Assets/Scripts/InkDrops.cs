using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkDrops : MonoBehaviour
{
    
    private PlayerHealth player;
    private bool startAlphaDecay;
    public float harm = 5;
    public float startDecayTimer;
    private float startDecayT =0;
    private bool startDestroy = false;
    private float decayT = 0;
    public float decayTimer;

    public bool StartAlphaDecay
    {
        get
        {
            return startAlphaDecay;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        startAlphaDecay = false;
    }

    // Update is called once per frame
    void Update()
    {
        startDecayT += Time.deltaTime;
        if (startDecayT > startDecayTimer)
        {
            startDestroy = true;
        }
        if (startDestroy)
        {
            decayT += Time.deltaTime;
            startAlphaDecay = true;
            if (decayT > decayTimer)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player.InkDamage(harm);
        }
    }
}
