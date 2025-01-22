using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidCharacter : MonoBehaviour
{
    [Header("Movement")]

    public float walkSpeed;
    public float groundDrag;
    public float angleRight = -90;
    public float angleLeft = 90;
    private float angle;
    private float moveSpeed;
    public float velGiro;
    float transicion;
    

    [Header("Climbing")]
    public float climbSpeed;
    private float climbMovement;
    public bool climbing;



    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float additionalGravity = 10f;
    bool readyToJump;
    public bool jumping;
    [Header("Crouching")]
    public float crouchSpeed;
    //public float crouchYScale;
    //private float startYScale;



    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftShift;
    


    [Header("Collision Check")]
    public float playerHeight;
    public float playerWidth;
    public LayerMask whatIsGround;
    public float distanceLadder;
    
    public float sphereCastRadius;
   

    public bool grounded;
    private bool ladder;
    private RaycastHit ladderHit;
    public Vector3 direction;
    //private detectEnemy detectE;
    public PlayerHealthUI pHealth;


    //[Header("Slope Handling")]
    //public float maxSlopeAngle;
    //private RaycastHit slopeHit;
    //private bool exitingSlope;

    [Header("Anim")]
    public animCharacter animC;

    [Header("Enemies")]
    public GameObject brazo;
    public detectEnemies dEnem;
    public float distanceForParry;
    public bool parry;
    public bool parryTutorial;
    float timer;
    public float timeAttack = 1f;
    bool attack;
    public bool quitarVida;
    public bool lookingEnemy;

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
        //detectE = GameObject.Find("CHARACTER").GetComponent<detectEnemy>();
        angle = angleRight;
        timer = timeAttack;
        attack = true;
        //startYScale = transform.localScale.y;



    }
    void Update()
    {
        MyInput();
        //SpeedControl();
        VelocityStateHandler();
        ClimbingStateHandler();
        WallCheck();
        EnemyCheck();
        
        //ladder check

        if (climbing) ClimbingMovement();

        //ground check
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.2f, whatIsGround);





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
        
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0f);


    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            //continuous jump
            

        }
        //start crouch

        /*
         * if (Input.GetKeyDown(crouchKey))
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
        */


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
            jumping = false;
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

        //if (OnSlope() && !exitingSlope)
        //{
            
        //    rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 5f, ForceMode.Force);
        //    jumping = false;



        //}

        if (grounded)
        {
            rb.AddForce(movement * moveSpeed * 10f, ForceMode.Force);
            resetJump();
            
            jumping = false;
        }
        else 
        {
            rb.AddForce(movement * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            //turn gravity off while on Slope
            //rb.useGravity = !OnSlope();
            if(!climbing) jumping = true;
        }

        if (horizontalInput < 0)
        {
            if (angle < angleLeft)
            {
                angle += velGiro * Time.deltaTime * 100;
                movement.x = 0;
            }

            else
            {
                angle = angleLeft;
                movement.x = horizontalInput;
            }
                
            //transform.localEulerAngles = new Vector3(0, angle, 0);
            direction = Vector3.left;
            
        }
        else if (horizontalInput > 0)
        {
            if (angle > angleRight)
            {
                angle -= velGiro * Time.deltaTime * 100;
                movement.x = 0;
            }

            else
            {
                angle = angleRight;
                movement.x = horizontalInput;
            }
                
            //transform.localEulerAngles = new Vector3(0, angle, 0);
            direction = Vector3.right;
            
        }

    }
    //private void SpeedControl()
    //{
    //    // limit speed of slope
    //    if (OnSlope() && !exitingSlope)
    //    {
    //        if (rb.velocity.magnitude > moveSpeed)
    //            rb.velocity = rb.velocity.normalized * moveSpeed;
    //    }
    //    else
    //    {
    //        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, 0f);
    //        // limit velicity if needed;
    //        if (flatVel.magnitude > moveSpeed)
    //        {
    //            Vector3 limitedVel = flatVel.normalized * moveSpeed;
    //            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, rb.velocity.z);
    //        }
    //    }



    //}
    private void Jump()
    {

        //exitingSlope = true;
        // reset velocity;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }
    private void resetJump()
    {
        readyToJump = true;
        //exitingSlope = false;
    }
    //private bool OnSlope()
    //{
    //    if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.4f,whatIsGround))
    //    {
    //        float angleSlope = Vector3.Angle(Vector3.up, slopeHit.normal);
    //        return angleSlope < maxSlopeAngle && angleSlope != 0;
    //    }
    //    return false;
    //}
    //private Vector3 GetSlopeMoveDirection()
    //{
    //    return Vector3.ProjectOnPlane(movement, slopeHit.normal).normalized;
    //}
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
    private void EnemyCheck()
    {
        

        lookingEnemy = Physics.Raycast(transform.position, direction, out RaycastHit enemyHit, playerWidth * 0.5f + distanceForParry);

        if (Physics.Raycast(transform.position, direction, out enemyHit, playerWidth * 0.5f + distanceForParry))
        {
            if (!enemyHit.transform.TryGetComponent(out Enemy enemy))
            {
                lookingEnemy = false;
                brazo.SetActive(false);
            }
        }
        if (lookingEnemy)
        {
            brazo.SetActive(true);

            if (Input.GetMouseButtonDown(1)) parry = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (attack)
                {
                    timer = timeAttack;
                    dEnem.cargado = true;
                    quitarVida = true;
                    attack = false;

                }
            }
            else
            {
                quitarVida = false;
                
            }
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonDown(0) || !lookingEnemy) parry = false;

        if (!attack)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

            }
        }

        if (timer <= 0)
        {
            
            attack = true;
            dEnem.cargado = false;

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
   


    public void LifeCharacter() {

        if (parry)
        {
            parryTutorial = true;
            Debug.Log("bloqueo!!!");
            parry = false;
        }
        else
        {
            Debug.Log("auuuuuuu");
            pHealth.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
        }
    }


}