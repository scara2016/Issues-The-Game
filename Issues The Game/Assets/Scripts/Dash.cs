using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    enum DashState
    {
        DashStart,
        Dashing,
        Moving
    }
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;
    private DashState dashState;
    private Movement playerMovement;
    private Rigidbody2D rb;
    private PlayerControls playerControls;
    private Vector3 initialPlayerPos;
    private Vector3 finalPlayerPos;
    private float t = 0;
    private float fractionTravelled = 1;
    private AnimationController controller;
    // Start is called before the first frame update

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        controller = this.GetComponent<AnimationController>();
    }

    void Start()
    {
        dashState = DashState.Moving;
        playerMovement = GetComponent<Movement>();   
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        switch (dashState)
        {
            case DashState.DashStart:
                initialPlayerPos = transform.position;
                finalPlayerPos = transform.position + new Vector3(dashDistance, 0, 0);
                dashState = DashState.Dashing;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, playerControls.Main.Move.ReadValue<float>() * Vector2.right, dashDistance);
                fractionTravelled = 1f;
               // if (raycastHit2D.distance != 0)
                 //   {
                   //     fractionTravelled = raycastHit2D.distance / dashDistance;
                   // }
                
                break;
            case DashState.Dashing:
                    playerMovement.enabled = false;
                    Debug.Log("dashdistance " + fractionTravelled);
                    rb.MovePosition(Vector3.Lerp(initialPlayerPos, finalPlayerPos, t));
                    t += dashSpeed * Time.deltaTime;
                if (t >= fractionTravelled)
                {
                    fractionTravelled = 1;
                    t = 0; 
                    dashState = DashState.Moving;
                }
                    controller.DashState();
                break;

            case DashState.Moving:
                    playerMovement.enabled = true;
                    if(playerControls.Main.Dash.ReadValue<float>() == 1)
                    {
                        dashState = DashState.DashStart;
                    }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dashState = DashState.Moving;
    }
}
