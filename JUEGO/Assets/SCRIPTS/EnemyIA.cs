using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("AttackSettings")]
    public bool bringDown;
    public float coolDown;
    Vector3 direction;
    bool exitAttack;
    

    [Header("Movement")]
    public GameObject[] turnLeft;
    public GameObject[] turnRight;
    public GameObject[] limits;
    public float EnemySpeed = 2;
    public float velGiro;
    public float angleLeft;
    public float angleRight;
    public float angle;
    public bool goingRight;
    public float wanderingTimer;
    public float chasingTimer;
    float timer;
    float timer2;
    float timer3;
    bool stop;
    bool stopChase;
    bool stopAttack;
    
    

    [Header("Detection")]
    public RigidCharacter rigidCharacter;
    bool playerInSightRange;
    bool playerAttackRange;
    public float rangeAttack;
    public float rangeChase;
    public LayerMask whatIsPlayer;


    

    [Header("Life")]
    public float life;
    bool die;

    void Start()
    {
        timer = coolDown;
        timer2 = wanderingTimer;
        timer3 = chasingTimer;
        die = false;
        


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
        if (stopChase)
        {
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

        if (!stop) transform.Translate(Vector3.left * EnemySpeed * Time.deltaTime);

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

       

    }
    private void Chasing()
    {
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

        if (!stop) transform.Translate(Vector3.left * EnemySpeed * Time.deltaTime);

        for (int i = 0; i < limits.Length; i++)
        {
            if (Vector3.Distance(transform.position, limits[i].transform.position) < 0.5f)
            {
                stop = true;
            }
        }
        // Atack setup
        bringDown = false;
        timer = coolDown;
        // Wandering setup
        stopChase = true;
        timer2 = wanderingTimer;

    }
    private void Attacking()
    {
        if (!exitAttack) StartCoroutine(Attack());
        else bringDown = false;


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
        timer3 = chasingTimer;
    }
    private IEnumerator Attack()
    {
        bringDown = true;
        timer = coolDown;
        //cambiar timer por valores random
        exitAttack = true;
        yield return new WaitForSeconds(0.1f);
        rigidCharacter.SendMessage("LifeCharacter", SendMessageOptions.DontRequireReceiver);

    }

    public void EndLife()
    {
        die = true;
        
    }

    private IEnumerator Dying()
    {
        transform.localEulerAngles = new Vector3(0, 0, -75);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    



}
