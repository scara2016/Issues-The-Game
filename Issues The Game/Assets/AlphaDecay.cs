using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaDecay : MonoBehaviour
{
    private InkDrops parentInkDrop;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        parentInkDrop = transform.parent.GetComponent<InkDrops>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentInkDrop.StartAlphaDecay)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a-Time.deltaTime);
        }
    }
}
