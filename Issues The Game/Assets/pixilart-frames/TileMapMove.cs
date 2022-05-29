using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapMove : MonoBehaviour
{
    enum InkState
    {
        findingNext,
        movingToNext,
        expandingOverPanel,
        waitingMenacingly,
        stop
    }
    private InkStartObject inkStartObject;
    public List<StopPointData> StopPoints;
    public bool vertical;
    public float inkSpeed;

    private float startPosition;
    private float gridStartPosition;
    private float startObjectStartPosition;
    private float finalPosition;
    private float distance;
    private InkState inkState;
    private int currentTarget = 0;
    private float t = 0;
    private float waitT=0;
    private Timer timer;
    private TilemapCollider2D tilemapCollider2D;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        inkState = InkState.findingNext;
        inkStartObject = FindObjectOfType<InkStartObject>();
        timer.StartTimer();
        tilemapCollider2D = GetComponent<TilemapCollider2D>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget<StopPoints.Count)
        switch (inkState)
        {

            case InkState.findingNext:
                    timer.StartTimer();
                    timer.timeRemaining = StopPoints[currentTarget].TimerToReach;
                    Debug.Log("FindingNext");
                if (vertical)
                {
                    startPosition = inkStartObject.transform.localPosition.y;
                        
                    finalPosition = StopPoints[currentTarget].transform.localPosition.y;
                        distance = finalPosition - startPosition;
                        gridStartPosition = transform.position.y;
                        startObjectStartPosition = inkStartObject.transform.position.y;
                    }
                else
                {
                    startPosition = inkStartObject.transform.localPosition.x;
                    finalPosition = StopPoints[currentTarget].transform.localPosition.x;
                        distance = finalPosition - startPosition;
                        gridStartPosition = transform.position.x;
                        startObjectStartPosition = inkStartObject.transform.position.x;
                    }

                    inkState = InkState.waitingMenacingly;
                break;

            case InkState.waitingMenacingly:
                    Debug.Log("Waiting");
                    waitT +=Time.deltaTime;
                if (waitT >= StopPoints[currentTarget].TimerToReach)
                {
                    waitT = 0;

                    inkState = InkState.movingToNext;
                }
                break;

            case InkState.movingToNext:
                    Debug.Log("Moving");
                    if (distance >= 0)
                        t += Time.deltaTime * inkSpeed;
                    else
                        t -= Time.deltaTime * inkSpeed;
                    if (vertical)
                    {
                        transform.position = new Vector3(transform.position.x, gridStartPosition + t, transform.position.z);
                       inkStartObject.transform.position = new Vector3(transform.position.x, startObjectStartPosition + t, transform.position.z);

                    }
                    else
                    {
                       transform.position = new Vector3(gridStartPosition + t, transform.position.y, transform.position.z);
                        inkStartObject.transform.position = new Vector3(startObjectStartPosition + t, transform.position.y, transform.position.z);
                    }
                    if (distance >= 0) {
                        if (t >= distance)
                        {
                            t = 0;
                            currentTarget++;
                            inkState = InkState.findingNext;
                        }
                    }
                    else
                    {
                        if (t <= distance)
                        {
                            t = 0;
                            currentTarget++;
                            inkState = InkState.findingNext;
                        }
                    }
                break;
            case InkState.stop:
                 break;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        playerHealth.health = 0;
        StopInk();
        
    }
    public void UpdateWaitT(float timeTodAdd)
    {
        waitT -= timeTodAdd;
    }

    public void StopInk()
    {
        inkState = InkState.stop;
    }

}
