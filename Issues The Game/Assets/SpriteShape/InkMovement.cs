using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InkMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteShapeController spriteShapeController;
    private PolygonCollider2D shapeCollider;
    private float TAU = Mathf.PI*2;
    public GameObject target;
    private Spline spline;
    public float numberOfPoints = 20;
    public float radius = 1;
    private float smoothness = 0.2f;
    public float noiseSize = 2f;
    public float waveMax=10;
    public float waveMin = 3;
    public float generalExpandSpeed = 0.2f;
    private int numberOfPointsThatFollow;
    List<int> followingPoints = new List<int>();
    List<GameObject> targets = new List<GameObject>();
    private int currentTargetIndex=0;
    private Vector3 goal;
    void Start()
    {
        
        targets.AddRange(GameObject.FindGameObjectsWithTag("InkTarget"));
        numberOfPoints = numberOfPoints - 4;
        spriteShapeController = GetComponent<SpriteShapeController>();
        shapeCollider = GetComponent<PolygonCollider2D>();
        spline = spriteShapeController.spline;
        numberOfPointsThatFollow = ((int)numberOfPoints+4) / 2;
        //   spline.InsertPointAt(1, Vector3.Lerp(spline.GetPosition(0), spline.GetPosition(1), 0.5f));
        Vector3 firstNodePos = spline.GetPosition(0);
        Vector3 secondNodePos = spline.GetPosition(1);
        Vector3 thirdNodePos = spline.GetPosition(2);
        Vector3 fourthNodePos = spline.GetPosition(3);
        Vector3[] nodePositions = new Vector3[(int)numberOfPoints];
        
        // Generate initial positions
        for(int i=1; i <= numberOfPoints; i++)
        {
            if(i<=numberOfPoints/4)
                nodePositions[i-1] = Vector3.Lerp(firstNodePos,secondNodePos, i / ((numberOfPoints / 4) + 1));
            else if (i <= 2 * numberOfPoints / 4)
                nodePositions[i - 1] = Vector3.Lerp(secondNodePos, thirdNodePos, (i - ((numberOfPoints / 4))) / ((numberOfPoints / 4) + 1));
            else if (i <= 3 * numberOfPoints / 4)
                nodePositions[i - 1] = Vector3.Lerp(thirdNodePos, fourthNodePos, (i - 2*((numberOfPoints / 4))) / ((numberOfPoints / 4) + 1));
            else if (i <= 4 * numberOfPoints / 4)
                nodePositions[i - 1] = Vector3.Lerp(fourthNodePos, firstNodePos, (i - 3* ((numberOfPoints / 4))) / ((numberOfPoints / 4) + 1));

        }

        //create points at initial positions
        int nodeCounter = 0;
        for (int i = 1; i <= numberOfPoints+4; i++)
        {
            if ((i) % ((numberOfPoints+4)/4) != 0)
            {
                spline.InsertPointAt(i, nodePositions[nodeCounter]);
                nodeCounter++;
            }
        }
        //set all points to continous curve mode
        for(int i = 0; i < spline.GetPointCount(); i++)
        {
            spline.SetTangentMode(i, ShapeTangentMode.Continuous); 
        }

        double[] offsetsFlat = new double[(int)numberOfPoints + 4];
        double[] offsetsBumpy = new double[(int)numberOfPoints + 4];
        double[] offsetsAverage = new double[(int)numberOfPoints + 4];

        for (int i = 0; i < offsetsFlat.Length; i++)
        {
            offsetsFlat[i] = Mathf.PerlinNoise((float)i / (offsetsFlat.Length*2), 0.0f);
            offsetsBumpy[i] = (Mathf.Pow(Mathf.PerlinNoise((float)i / (offsetsFlat.Length), 0.0f),4))*10;
            offsetsAverage[i] = (offsetsBumpy[i] + offsetsFlat[i]) / 2;
            offsetsAverage[i] = (2 * offsetsAverage[i]) - 1;
            offsetsAverage[i] = System.Math.Pow(offsetsAverage[i],noiseSize);
        }
        double max = float.MinValue;
        double min = float.MaxValue;
        for(int i = 0; i < offsetsAverage.Length; i++)
        {
            if (offsetsAverage[i] > max)
                max = offsetsAverage[i];
            if (offsetsAverage[i] < min)
                min = offsetsAverage[i];
        }
        double OldRange = max - min;
        double NewRange = waveMax - waveMin;
        for(int i=0; i < offsetsAverage.Length; i++)
        {
            offsetsAverage[i] = (((offsetsAverage[i] - min) * NewRange) / OldRange)+waveMin;
            Debug.Log(offsetsAverage[i]);

        }

       
        //put the points on a circle
        float angle = TAU / (numberOfPoints+4);   
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            spline.SetPosition(i,((new Vector3(Mathf.Sin(angle*i), Mathf.Cos(angle*i), 0))*radius*(float)offsetsAverage[i]));
        }


        //assign tangents to the points for the curve(dont ask me how this works)
        Vector3 leftTangent;
        Vector3 rightTangent;

        for (int i = 0; i < spline.GetPointCount(); i++)
        {

            leftTangent = new Vector3(Mathf.Sin(angle*i - TAU/4), Mathf.Cos((angle*i - TAU / 4)), 0).normalized*smoothness;
            rightTangent = new Vector3(-Mathf.Sin((angle*i - TAU / 4)), -Mathf.Cos((angle*i - TAU / 4)), 0).normalized*smoothness;
            spline.SetLeftTangent(i, leftTangent);
            spline.SetRightTangent(i, rightTangent);
        }

        FindNextTarget();
        FollowTransform();
        // goal = target.transform.position - shapeCollider.bounds.center;  
    }

    private void FindNextTarget()
    {
        targets[currentTargetIndex].transform.position = goal;
        SetMovingPoints();
    }
    private Vector3 goalVector;
    int min;

    private void SetMovingPoints()
    {
        float minDistance = float.MaxValue;
        for(int i = 0; i < spline.GetPointCount(); i++)
        {
            if(Vector3.Distance(goal,spline.GetPosition(i)) < minDistance)
            {
                minDistance = Vector3.Distance(goal, transform.position + spline.GetPosition(i));
                min = i;
            }
        }
        goalVector = goal - (transform.position + spline.GetPosition(min));

        numberOfPointsThatFollow = ((int)numberOfPoints / 2) + 1;
        for(int i = 0; i < numberOfPointsThatFollow; i++)
        {
            followingPoints.Insert(i, i + ((numberOfPointsThatFollow - 1) / 2));
        }
        
    }

    /*      
                                                                                                                            pepesmile
     */

    // Update is called once per frame
    void Update()
    {
       transform.localScale += Vector3.one * Time.deltaTime * generalExpandSpeed;       

    }

    private void FollowTransform()
    {
        //StartCoroutine(InkMoveCoroutine(followingPoints[0]));

         for(int i = 0; i < followingPoints.Count; i++)
         {
             StartCoroutine(InkMoveCoroutine(followingPoints[i]));
         }
        
    }
    float t = 1;
    IEnumerator InkMoveCoroutine(int indexToMove)
    {
        Vector3 initialPosition = spline.GetPosition(indexToMove);
        Vector3 placeToGo = (transform.position + spline.GetPosition(indexToMove))+goalVector;
        while(Vector3.Distance(spline.GetPosition(min), placeToGo) > 0.05) {
            spline.SetPosition(indexToMove, Vector3.Lerp(initialPosition, placeToGo, t*Time.deltaTime));
            
            yield return null;

        }

        yield return new WaitForSeconds(2f);
    }


}
