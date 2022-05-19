using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelManager : MonoBehaviour
{
    BoxCollider2D panelCollider;
    private List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> passableSurfaces = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        panelCollider = GetComponent<BoxCollider2D>();
        for(int i = 0; i < passableSurfaces.Count; i++)
        {
            passableSurfaces[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
                if (enemies.Count == 0)
                {
                    EnableSurfaces();
                }
            }
        }
        if (enemies.Count == 0)
        {
            for (int i = 0; i < passableSurfaces.Count; i++)
            {
                passableSurfaces[i].SetActive(true);
            }
        }
    }

    private void EnableSurfaces()
    {
        for (int i = 0; i < passableSurfaces.Count; i++)
        {
            passableSurfaces[i].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemies.Contains(collision.gameObject) && collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject);
        }
    }
    
}
