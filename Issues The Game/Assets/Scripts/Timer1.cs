using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float startTime;
    private float timeLeft;
    private TextMeshPro text;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        timeLeft = startTime;
    }


    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
    }
}
