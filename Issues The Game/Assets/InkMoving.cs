using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkMoving : MonoBehaviour
{

    enum InkState
    {
        findingNext,
        movingToNext,
        expandingOverPanel,
        waitingMenacingly
    }

    // Start is called before the first frame update
    public List<GameObject> panels;
    private InkState inkState;
    private PolygonCollider2D collider2D;
    private int panelNumber = 0;
    void Start()
    {
        collider2D = GetComponent<PolygonCollider2D>();
        inkState = InkState.movingToNext;
    }

    // Update is called once per frame
    void Update()
    {
        switch (inkState)
        {
            case InkState.findingNext:
                FindNextAndScale(IncomingDirection());
                break;
            case InkState.movingToNext:

                break;
            case InkState.expandingOverPanel:

                break;
            case InkState.waitingMenacingly:

                break;


        }

    }

    Vector2 IncomingDirection()
    {
        Vector2 roughDirection = new Vector2(panels[panelNumber].transform.position.x - panels[panelNumber + 1].transform.position.x, panels[panelNumber].transform.position.y - panels[panelNumber + 1].transform.position.y);
        if (Mathf.Abs(roughDirection.x) > Mathf.Abs(roughDirection.y)){
            return new Vector2(Mathf.Sign(roughDirection.x) * 1, 0);
        }
        else
        {
            return new Vector2(0, Mathf.Sign(roughDirection.y) * 1);
        }
    }

    float FindNextAndScale(Vector2 incomingDirection)
    {
        float scaleRatio;
        panelNumber++;

        if (Mathf.Abs(incomingDirection.x) > Mathf.Abs(incomingDirection.y))
        {
            scaleRatio = panels[panelNumber].GetComponent<Collider2D>().bounds.size.y / collider2D.bounds.size.x;
        }
        return 0;
    }

}
