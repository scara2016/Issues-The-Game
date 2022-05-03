using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Collider2D currentPanelCollider;
    Camera cam;
    Vector3 startPos;
    bool moveCam = false;
    private float timeToReachTarget=0.5f;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        startPos = cam.gameObject.transform.position;
        t = 0;
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("panel"))
        {
            Debug.Log(collision.gameObject.tag);

            currentPanelCollider = collision;
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
            cam.gameObject.transform.position = Vector3.Lerp(startPos, currentPanelCollider.bounds.center, t);
            //cam.gameObject.transform.position = new Vector3(currentPanelCollider.bounds.center.x, currentPanelCollider.bounds.center.y, -10);
           // moveCam = false;
          }
    }
}
