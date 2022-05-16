using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
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

    public void SetMaxHealth(float Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
    }

    public void SetHealth(float Health)
    {
        slider.value = Health;
    }
}
