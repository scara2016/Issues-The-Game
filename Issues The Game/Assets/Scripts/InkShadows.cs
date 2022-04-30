using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkShadows : MonoBehaviour
{
    // Start is called before the first frame update
    private float dropTime = 0f;
    public float timeTillDrop = 1f;
    public InkDrops inkDrop;
    public Vector3 finalScale = new Vector3(3f,3f,3f);
    public float initialScaleSize;
    private Vector3 initialScale;
    void Start()
    {
        initialScale = new Vector3(initialScaleSize, initialScaleSize, initialScaleSize);
        transform.localScale = initialScale;
    }

    // Update is called once per frame
    void Update()
    {
        dropTime += Time.deltaTime;
        if(dropTime >= timeTillDrop)
        {
            GenInkDrops();
            Destroy(this.gameObject);
        }

        this.transform.localScale = Vector3.Lerp(initialScale, finalScale, dropTime / timeTillDrop);
    }

    private void GenInkDrops()
    {
        Vector2 spawnPosition = this.transform.position;
        InkDrops clone = Instantiate(inkDrop, spawnPosition, Quaternion.identity);
        clone.transform.localScale = finalScale;
    }

}
