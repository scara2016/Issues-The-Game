using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Collider2D currentPanelCollider;
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
        if (collision.gameObject.tag.Equals("panel"))
        {
            currentPanelCollider = collision;
            moveCam = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        if (moveCam == true)
        {
            cam.gameObject.transform.position = Vector3.Lerp(startPos, currentPanelCollider.bounds.center, t);
        }
        Debug.Log(currentPanelCollider.bounds.Contains(Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0))));
        if (currentPanelCollider.bounds.Contains(Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0))))
        {
            CamFollowPlayer();
        }
    }

    private void CamFollowPlayer()
    {
        cam.gameObject.transform.position = Vector3.Lerp(startPos, new Vector3(transform.position.x,currentPanelCollider.bounds.center.y,cam.transform.position.z), t);
    }

}
