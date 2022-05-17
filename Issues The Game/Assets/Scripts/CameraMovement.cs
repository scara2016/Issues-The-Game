using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Collider2D currentPanelCollider;
    Camera cam;
    public float cameraDepth = -10f;
    Vector3 startPos;
    bool moveCam = false;
    private float timeToReachTarget=0.5f;
    private float t;
    bool centralized;
    bool horizontal;
    bool freePan;
    float cameraWidth;
    float cameraHeight;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
            
            cameraWidth = Vector3.Distance(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)), cam.ViewportToWorldPoint(new Vector3(1, 0, 0)));
            cameraHeight = Vector3.Distance(cam.ViewportToWorldPoint(new Vector3(0, 0, 0)), cam.ViewportToWorldPoint(new Vector3(0, 1, 0)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        startPos = cam.gameObject.transform.position;
        t = 0;
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("panel"))
        {
            currentPanelCollider = collision;
            checkCentralized(collision);
            moveCam = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
       // Debug.Log(moveCam);
        if (moveCam == true)
        {
            if (centralized)
            {
                cam.gameObject.transform.position = Vector3.Lerp(startPos,new Vector3(currentPanelCollider.bounds.center.x, currentPanelCollider.bounds.center.y, cameraDepth), t);
            }
            else if (!centralized)
            {
                if (freePan)
                {
                    cam.gameObject.transform.position = Vector3.Lerp(startPos, new Vector3(this.transform.position.x, this.transform.position.y, cameraDepth), t);
                }
                else if (horizontal)
                {
                    cam.gameObject.transform.position = Vector3.Lerp(startPos,new Vector3(this.transform.position.x, currentPanelCollider.bounds.center.y, cameraDepth), t);
                }
                else
                {
                    cam.gameObject.transform.position = Vector3.Lerp(startPos, new Vector3(currentPanelCollider.bounds.center.x, this.transform.position.y, cameraDepth), t);
                }
            }
        }
    }

    void checkCentralized(Collider2D panelCollider)
    {
        if(panelCollider.bounds.size.x >= cameraWidth && panelCollider.bounds.size.y >= cameraHeight)
        {
            freePan = true;
        }
        else if (panelCollider.bounds.size.x >= cameraWidth)
        {
            centralized = false;
            horizontal = true;
            freePan = false;
        }
        else if(panelCollider.bounds.size.y >= cameraHeight)
        {
            centralized = false;
            horizontal = false;
            freePan = false;
        }
        else
        {
            centralized = true;
            freePan = false;
        }
            
    }

}
