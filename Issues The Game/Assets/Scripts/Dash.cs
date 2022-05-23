using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    enum DashState
    {
        DashStart,
        Dashing,
        Moving,
        
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

    public float dashCoolDown;
    private float dashT;
    private bool dashCoolDownStart;

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
                finalPlayerPos = transform.position + new Vector3(playerControls.Main.Move.ReadValue<float>() * dashDistance, 0, 0);
                dashState = DashState.Dashing;
                fractionTravelled = 1f;
                
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
                if(dashCoolDownStart)
                    dashT += Time.deltaTime;
                if (dashT > dashCoolDown)
                {
                    dashCoolDownStart = false;
                    dashT = 0;
                }
                    if(playerControls.Main.Dash.ReadValue<float>() == 1 && !dashCoolDownStart)
                    {
                        dashState = DashState.DashStart;
                    dashCoolDownStart = true;
                    }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dashState = DashState.Moving;
    }
}
