using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    private PlayerHealth playerHealth;

    private static Healthbar instance;
    public static Healthbar Instance
    {
        get
            {
                if (instance == null)
                {
                    Debug.LogError("No HealthBar in the scene");
                }
                return instance;
            }
    }
    public Slider slider;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();       
    }

    private void Start()
    {
        SetMaxHealth();
    }

    private void Update()
    {
        SetHealth();
    }

    private void SetMaxHealth()
    {
        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.health;
    }

    private void SetHealth()
    {
        slider.value = playerHealth.health;
    }
}
