using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("AttackSettings")]
    
    public float coolDown;
    Vector3 direction;
    bool exitAttack;
    

    [Header("Movement")]
    public GameObject[] turnLeft;
    public GameObject[] turnRight;
    public GameObject[] limits;
    public float EnemySpeedWandering = 2;
    public float EnemySpeedChasing = 3;
    public float velGiro;
    public float angleLeft;
    public float angleRight;
    public float angle;
    public bool goingRight;
    public float wanderingTimer;
    public float chasingTimer;
    public float spotTimer;
    public float timer;
    float timer2;
    float timer3;
    float timer4;   
    bool stop;
    bool stopChase;
    bool stopAttack;
    bool stopSpoting;
    
    

    [Header("Detection")]
    
    public detectCharacter detectC;
    
    bool playerInSightRange;
    bool playerAttackRange;
    public float rangeAttack;
    public float rangeChase;
    private float rangeChaseSaved;
    public LayerMask whatIsPlayer;


    

    [Header("Life")]
    public float life;



    [Header("Animation references")]

    public bool patrol;
    public bool spot;
    public bool range;
    public bool attackAction;
    public bool die;


    void Start()
    {
        timer = coolDown;
        timer2 = wanderingTimer;
        timer3 = chasingTimer;
        die = false;
        exitAttack = true;
        rangeChaseSaved = rangeChase;
        rangeChase = 4f;



    }


    void Update()
    {

        RaycastHit hitInfo;
        if (angle < 0) direction = Vector3.right;
        else direction = Vector3.left;

        playerInSightRange = Physics.SphereCast(transform.position, 0.1f, direction, out hitInfo, rangeChase, whatIsPlayer);
        playerAttackRange = Physics.SphereCast(transform.position, 0.1f, direction, out hitInfo, rangeAttack, whatIsPlayer);

        if (!die)
        {
            if (!playerInSightRange && !playerAttackRange) Wandering();
            if (playerInSightRange && !playerAttackRange) Chasing();
            if (playerInSightRange && playerAttackRange) Attacking();
        }
        else
        {
            StartCoroutine(Dying());
        }
        

        




    }
    void FixedUpdate()
    {
        if (!goingRight)
        {
            if (angle < angleLeft)
            {
                stop = true;
                angle += velGiro * Time.deltaTime * 100;
            }
            else
            {
                angle = angleLeft;
                stop = false;
            }


            transform.localEulerAngles = new Vector3(0, angle, 0);




        }
        else if (goingRight)
        {
            if (angle > angleRight)
            {
                stop = true;
                angle -= velGiro * Time.deltaTime * 100;
            }

            else
            {
                angle = angleRight;
                stop = false;
            }

            transform.localEulerAngles = new Vector3(0, angle, 0);


        }
    }
    private void Wandering()
    {
        detectC.cargado = false;

        if (stopChase)
        {
            range = true;

            if (timer2 > 0)
            {
                stop = true;
                timer2 -= Time.deltaTime;
            }
            else
            {
                goingRight = !goingRight;
                stop = false;
                stopChase = false;

            }
        }
        else
        {
            range = false;
        }

        if (!stop) transform.Translate(Vector3.left * EnemySpeedWandering * Time.deltaTime);

        for (int i = 0; i < turnRight.Length; i++)
        {
            if (Vector3.Distance(transform.position, turnRight[i].transform.position) < 0.5f)
            {
                goingRight = true;
            }
        }

        for (int i = 0; i < turnLeft.Length; i++)
        {
            if (Vector3.Distance(transform.position, turnLeft[i].transform.position) < 0.5f)
            {
                goingRight = false;
            }
        }

        timer4 = spotTimer;
        if (!spot) stopSpoting = true;
        else if(spot) stopSpoting = false;


    }
    private void Chasing()
    {
        detectC.cargado = false;
        
        spot = true;

        if (stopSpoting)
        {

            if (timer4 > 0)
            {
                stop = true;
                timer4 -= Time.deltaTime;
            }
            else
            {
                stop = false;
                stopSpoting = false;
                rangeChase= rangeChaseSaved;
            }
        }

        if (stopAttack)
        {

            if (timer3 > 0)
            {
                stop = true;
                timer3 -= Time.deltaTime;
            }
            else
            {
                stop = false;
                stopAttack = false;
                
            }
        }

        if (!stop)
        {
            transform.Translate(Vector3.left * EnemySpeedChasing * Time.deltaTime);
            range = false;
        }
        else if (!stopSpoting) range = true;

        for (int i = 0; i < limits.Length; i++)
        {
            if (Vector3.Distance(transform.position, limits[i].transform.position) < 0.5f)
            {
                stop = true;
            }
        }
        // Atack setup
        attackAction = false;
        timer = coolDown;
        // Wandering setup
        stopChase = true;
        timer2 = wanderingTimer;

    }
    private void Attacking()
    {
        
        range = true;
        if (!exitAttack) Attack();
        else
        {
            attackAction = false;
            
        }


        if (exitAttack)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                exitAttack = false;
            }
        }
        // Chasing setup
        stopAttack = true;
        stopChase = true;
        timer3 = chasingTimer;
    }
    private void Attack()
    {
        attackAction = true;
        //cambiar timer por valores random
        timer = coolDown;
        detectC.cargado = true;
        exitAttack = true;
        
        

    }

    public void EndLife()
    {
        die = true;
        
    }

    private IEnumerator Dying()
    {
        
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    



}
