using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapMove : MonoBehaviour
{
    enum InkState
    {
        findingNext,
        movingToNext,
        expandingOverPanel,
        waitingMenacingly
    }

    public List<StopPointData> StopPoints;
    public bool vertical;
    private float startPosition;
    private float currentPosition;
    private float finalPosition;
    private InkState inkState;
    private int currentTarget = 0;
    private float t;
    private float waitT=0;
    // Start is called before the first frame update
    void Start()
    {
        inkState = InkState.findingNext;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget<StopPoints.Count)
        switch (inkState)
        {
            case InkState.findingNext:
                if (vertical)
                {
                    startPosition = transform.position.y;
                    finalPosition = StopPoints[currentTarget].transform.position.y;
                }
                else
                {
                    startPosition = transform.position.x;
                    finalPosition = StopPoints[currentTarget].transform.position.x;
                }
                
                inkState = InkState.waitingMenacingly;
                break;
            case InkState.waitingMenacingly:
                waitT+=Time.deltaTime;
                if (waitT >= StopPoints[currentTarget].TimerToReach)
                {
                    waitT = 0;
                    inkState = InkState.movingToNext;
                }
                break;
            case InkState.movingToNext:
                currentPosition = Mathf.Lerp(startPosition, finalPosition, t);
                if (vertical)
                    transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
                else
                    transform.position = new Vector3(currentPosition, transform.position.y, transform.position.z);
                t += Time.deltaTime*0.1f;
                if (t >= 1)
                {
                        currentTarget++;
                    inkState = InkState.findingNext;
                }
                break;

        }
    }
}
