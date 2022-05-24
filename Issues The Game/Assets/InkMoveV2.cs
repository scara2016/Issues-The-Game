using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InkMoveV2 : MonoBehaviour
{

    public int numberOfPoints;
    private SpriteShapeController spriteShapeController;
    public float distanceBetweenPoints;
    public float waveSpeed;
    private float[] waveSpeedPerPoint;

    private float highestPoint=0;
    private float lowestPoint=0;
    // Start is called before the first frame update
    void Start()
    {
        waveSpeedPerPoint = new float[numberOfPoints];
        for(int i=0; i < numberOfPoints; i++)
        {
            waveSpeedPerPoint[i] = waveSpeed;
            Debug.Log(waveSpeedPerPoint[i]);
        }

        spriteShapeController = GetComponent<SpriteShapeController>();
        spriteShapeController.spline.SetPosition(2,spriteShapeController.spline.GetPosition(2) + Vector3.right * numberOfPoints*distanceBetweenPoints);
        spriteShapeController.spline.SetPosition(3,spriteShapeController.spline.GetPosition(3) + Vector3.right * numberOfPoints*distanceBetweenPoints);

        for(int i = 0; i < numberOfPoints; i++)
        {
            float xPos = spriteShapeController.spline.GetPosition(index: i+1).x + distanceBetweenPoints;
            float yPos = 1 * Mathf.PerlinNoise(Random.Range(5f, 15f), 0 );
            spriteShapeController.spline.InsertPointAt(index: i + 2, point: new Vector3(xPos,yPos));
            if (yPos > highestPoint)
            {
                highestPoint = yPos;
            }
            else if (yPos < lowestPoint)
            {
                lowestPoint = yPos;
            }
        }

        for(int i = 2; i < numberOfPoints + 2; i++)
        {
            spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            spriteShapeController.spline.SetLeftTangent(i,new Vector3(-1,0,0));
            spriteShapeController.spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i=2;i<numberOfPoints+2;i++){
            spriteShapeController.spline.SetPosition(i, new Vector3(spriteShapeController.spline.GetPosition(i).x, spriteShapeController.spline.GetPosition(i).y + (waveSpeedPerPoint[i-2]*Time.deltaTime)));

            if (spriteShapeController.spline.GetPosition(i).y <= lowestPoint)
            {
                waveSpeedPerPoint[i-2] = -1 * waveSpeedPerPoint[i-2];
            }
            else if(spriteShapeController.spline.GetPosition(i).y >= highestPoint)
            {
                waveSpeedPerPoint[i-2] = -1 * waveSpeedPerPoint[i-2];    
            }
        }
    }

    

}
