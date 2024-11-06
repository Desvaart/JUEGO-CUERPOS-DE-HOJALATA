using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidCharacter : MonoBehaviour
{
    [Header("Movement")]

    public float walkSpeed;
    public float groundDrag;
    private float angleRight;
    private float angleLeft;
    private float moveSpeed;
    public float velGiro;

    [Header("Climbing")]
    public float climbSpeed;
    private float climbMovement;
    private bool climbing;



    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float additionalGravity = 10f;
    bool readyToJump;
    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;



    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftShift;
    


    [Header("Collision Check")]
    public float playerHeight;
    public float playerWidth;
    public LayerMask whatIsGround;
    public float distanceLadder;
    public float sphereCastRadius;
    public LayerMask whatIsLadder;

    private bool grounded;
    private bool ladder;
    private RaycastHit ladderHit;
    public Vector3 direction;
    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    private float horizontalInput;
    private float verticalInput;



    Vector3 movement;
    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        crouching,
        air
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;
        angleRight = -90;
        angleLeft = -90;


    }
    void Update()
    {
        MyInput();
        SpeedControl();
        VelocityStateHandler();
        ClimbingStateHandler();
        WallCheck();
        //ladder check

        if (climbing) ClimbingMovement();

        //ground check
        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position, 0.15f, Vector3.down, out hit, playerHeight * 0.5f + 0.2f, whatIsGround);
        

        //handle drag

        if (grounded)
            rb.drag = groundDrag;

        else
            rb.drag = 0;

       



    }
    void FixedUpdate()
    {
        MovePlayer();
        if (!ladder)
        {
            rb.AddForce(Vector3.down * additionalGravity, ForceMode.Acceleration);
        }
        

    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput < 0)
        {
            if (angleLeft < 90)
                angleLeft += velGiro * Time.deltaTime * 60;

            transform.localEulerAngles = new Vector3(0, angleLeft, 0);
            direction = Vector3.left;
            angleRight = 90;
        }
        else if (horizontalInput>0)
        {
            if (angleRight > -90)
                angleRight -= velGiro * Time.deltaTime * 60;
            transform.localEulerAngles = new Vector3(0, angleRight, 0);
            direction = Vector3.right;
            angleLeft = -90;
        }


        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            //continuous jump
            Invoke(nameof(resetJump), jumpCooldown);

        }
        //start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }


    }
    private void VelocityStateHandler()
    {
        // Mode - Walking
        if (grounded && Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
            // Mode - Crouching
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        // Mode - Air
        else
        {
            state = MovementState.air;

        }
    }
    private void ClimbingStateHandler()
    {
        if(ladder)
        {
            StartClimbing();
            if (verticalInput!=0)
            {
               if (verticalInput > 0)
                {
                    climbMovement = climbSpeed;
                } 
                else if (verticalInput < 0)
                {
                    climbMovement = -climbSpeed;
                }
            }
            else
            {
                climbMovement = 0f;
            }
        
        }
        else
        {
            StopClimbing();
        }
    }



    private void MovePlayer()
    {

        movement.x = horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 12f, ForceMode.Force);


        }

        if (grounded)
        {
            rb.AddForce(movement * moveSpeed * 10f, ForceMode.Force);
            resetJump();
        }
        else if (!grounded)
        {
            rb.AddForce(movement * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            //turn gravity off while on Slope
            rb.useGravity = !OnSlope();
        }
    }
    private void SpeedControl()
    {
        // limit speed of slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, 0f);
            // limit velicity if needed;
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, rb.velocity.z);
            }
        }



    }
    private void Jump()
    {

        exitingSlope = true;
        // reset velocity;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }
    private void resetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(movement, slopeHit.normal).normalized;
    }
    private void WallCheck()
    {
        ladder = Physics.SphereCast(transform.position, sphereCastRadius, direction, out ladderHit, playerWidth * 0.5f + distanceLadder);
        
        if(Physics.SphereCast(transform.position, sphereCastRadius, direction, out ladderHit, playerWidth * 0.5f + distanceLadder)) 
        { 
            if (!ladderHit.transform.TryGetComponent(out Escalable escalable)) 
            { 
                ladder=false; 
            }
        }

    }
    private void StartClimbing()
    {
        rb.useGravity = false;
        climbing = true;
        
        //camera fov change

    }
    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbMovement, rb.velocity.z);
    }
    private void StopClimbing()
    {
        rb.useGravity = true;
        climbing= false;
    }
}