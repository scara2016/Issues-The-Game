using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InkMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteShapeController spriteShapeController;
    private float TAU = Mathf.PI*2;
    public GameObject target;
    private Spline spline;
    public float numberOfPoints = 20;
    public float radius = 1;
    private float smoothness = 0.2f;
    public float noiseSize = 2f;
    public float waveMax=10;
    public float waveMin = 3;
    void Start()
    {
        numberOfPoints = numberOfPoints - 4;
        spriteShapeController = GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;

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


            /*
            leftTangent = Quaternion.Euler(0, 0, Mathf.Rad2Deg*angle) * (spline.GetPosition(i));
            rightTangent = Quaternion.Euler(0, 0, Mathf.Rad2Deg*angle) * (spline.GetPosition(i));
            spline.SetLeftTangent(i, leftTangent);
            spline.SetRightTangent(i, rightTangent);
            */
        }
        

        

        

    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
