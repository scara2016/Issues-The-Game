using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public float inkDripRate=2f;
    public InkShadows inkShadows;
    public GameObject inkDrips;
    private ArrayList inkShadowList = new ArrayList();
    private float inkDripCoolDownTimer;
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        inkDripCoolDownTimer += Time.deltaTime;
        if (inkDripCoolDownTimer > inkDripRate)
        {
            GenInkShadows();
            inkDripCoolDownTimer = 0;
        }
    }

    private void GenInkShadows()
    {
        Vector2 spawnPosition;
        //spawnPosition.x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width)).x);
        spawnPosition.y = Random.Range
                        (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        spawnPosition.x = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        InkShadows clone = Instantiate(inkShadows, spawnPosition, Quaternion.identity);

        inkShadowList.Add(clone);
    }

    private void GenInk()
    {
        GameObject clone = Instantiate(inkDrips, transform);
    }

}
