using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTranferScript : MonoBehaviour
{
    public WallTranferScript otherSide;
    private Collider2D otherSideCollider;
    private Collider2D initialCollider;
    public bool horizontal;
    public bool vertical = true;
    // Start is called before the first frame update
    void Start()
    {
        initialCollider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        otherSideCollider = otherSide.GetComponent<Collider2D>();
    }

    public Vector2 returnNewPosition(Vector3 playerPosition)
    {
        Vector2 finalPosition = new Vector2();
            Vector2 movementDifference = new Vector2(playerPosition.x, playerPosition.y) - (new Vector2(initialCollider.bounds.center.x, initialCollider.bounds.center.y));

        if (vertical)
        {
            finalPosition = new Vector2(otherSideCollider.bounds.center.x - movementDifference.x, otherSideCollider.bounds.center.y + movementDifference.y);
        }
        else if (horizontal) {
            finalPosition = new Vector2(otherSideCollider.bounds.center.x + movementDifference.x, otherSideCollider.bounds.center.y - movementDifference.y);
        }

        return finalPosition;
    }

}