using System;
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
    private int panelNumber = -1;
    private Vector3 initialPosition;
    private Vector3 endPosition;
    private Vector3 initialScale;
    private Vector3 Expansion;
    private float t = 0;
    public float waitingTimer;
    private float waitingT=0;
    public float expansionTime;
    private float expansionT;
    private float scaleRatio = 1;

    void Start()
    {
        collider2D = GetComponent<PolygonCollider2D>();
        inkState = InkState.findingNext;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (inkState)
        {
            case InkState.findingNext:
                t = 0;
                FindNextAndScale(IncomingDirection());
                initialPosition = transform.position;

                inkState = InkState.movingToNext;
                break;
            case InkState.movingToNext:
                MoveInk();
                if(t>=1)
                inkState = InkState.waitingMenacingly;
                break;
            case InkState.waitingMenacingly:
                waitingT += Time.deltaTime;
                if (waitingT > waitingTimer)
                {
                    waitingT = 0;
                    inkState = InkState.findingNext;
                }
                break;

        }

    }

    private void MoveInk()
    {
        endPosition.z = 0;
        transform.position = Vector3.Lerp(initialPosition, endPosition, t += Time.deltaTime);
    }

    Vector2 IncomingDirection()
    {
        Vector2 roughDirection = new Vector2(transform.position.x - panels[panelNumber + 1].transform.position.x, transform.position.y - panels[panelNumber + 1].transform.position.y);
        if (panelNumber >= 0)
        {
            roughDirection = new Vector2(panels[panelNumber].transform.position.x - panels[panelNumber + 1].transform.position.x, panels[panelNumber].transform.position.y - panels[panelNumber + 1].transform.position.y);
        }
         if (Mathf.Abs(roughDirection.x) > Mathf.Abs(roughDirection.y))
        {
            return new Vector2(Mathf.Sign(roughDirection.x) * 1, 0);
        }
        else
        {
            return new Vector2(0, Mathf.Sign(roughDirection.y) * 1);
        }
    }

    void FindNextAndScale(Vector2 incomingDirection)
    {
        panelNumber++;
  //      transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (Mathf.Abs(incomingDirection.x) > Mathf.Abs(incomingDirection.y))
        {
            
            scaleRatio = collider2D.bounds.size.x / panels[panelNumber].GetComponent<Collider2D>().bounds.size.y;
            if (incomingDirection.x<0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                endPosition = panels[panelNumber].transform.position + new Vector3(-panels[panelNumber].GetComponent<Collider2D>().bounds.extents.x, panels[panelNumber].GetComponent<Collider2D>().bounds.extents.y);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                endPosition = panels[panelNumber].transform.position + new Vector3(panels[panelNumber].GetComponent<Collider2D>().bounds.extents.x,-panels[panelNumber].GetComponent<Collider2D>().bounds.extents.y);

            }
        }
        else 
        {
            
            scaleRatio = collider2D.bounds.size.x / panels[panelNumber].GetComponent<Collider2D>().bounds.size.x;
            if (incomingDirection.y < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                endPosition = panels[panelNumber].transform.position + new Vector3(-panels[panelNumber].GetComponent<Collider2D>().bounds.extents.x, -panels[panelNumber].GetComponent<Collider2D>().bounds.extents.y);

            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                endPosition = panels[panelNumber].transform.position + new Vector3(panels[panelNumber].GetComponent<Collider2D>().bounds.extents.x, panels[panelNumber].GetComponent<Collider2D>().bounds.extents.y);

            }
        }
        transform.localScale = new Vector3(scaleRatio, scaleRatio);
    }

}
