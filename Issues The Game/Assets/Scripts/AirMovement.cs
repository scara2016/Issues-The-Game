using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : MonoBehaviour
{
    [SerializeField] private float AirMovementSpeed = 4f;
    private PlayerControls playerController;
    //For airmovement
    public bool movingRight;
    public bool movingLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Controls speed in air
    // Flips player
    private void AirMomentum()
    {
        // if(!playerController.collisionDetection.IsGrounded)
    }
}
