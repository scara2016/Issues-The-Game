using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDecay : MonoBehaviour
{
    public float decayTimer;
    private bool startDecay = false;
    private float decayT;
    void Update()
    {
        if (startDecay)
        {
            Decay();
        }
    }

    private void Decay()
    {
        decayT += Time.deltaTime;
        if (decayT >= decayTimer)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        startDecay = true;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
