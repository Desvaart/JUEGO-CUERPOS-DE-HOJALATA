using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacionEnemy: MonoBehaviour
{
    private Animator otroAnimator;
    private Animator animator;
    public GameObject guardada;
    public GameObject sacada;
    public float timerCoger = 0.66f;
    public float timerGolpe = 0.66f;
    float timer;
    private bool cuentaAtras;
    public EnemyIA enemyIA;
    float lifeSaved;
    bool golpe;

    void Start()
    {

        otroAnimator = GameObject.Find("armaAbrir").GetComponent<Animator>();

        animator = GetComponent<Animator>();

        sacada.SetActive(false);
        guardada.SetActive(true);
        cuentaAtras = false;
        lifeSaved = enemyIA.life;
        timer = timerGolpe;

        //if (otroAnimator != null)
        //{
        //    Debug.Log("Se ha accedido correctamente al Animator del objeto.");
        //}
        //else
        //{
        //    Debug.LogError("No se pudo acceder al Animator del objeto.");
        //}
    }

    void Update()
    {

        //spot
        if (enemyIA.spot)
        {

            animator.SetBool("spot", true);
            cuentaAtras = true;

        }
        if (cuentaAtras)
        {
            if (timerCoger > 0)
            {
                timerCoger -= Time.deltaTime;
            }
            else
            {
                sacada.SetActive(true);
                guardada.SetActive(false);
                otroAnimator.SetBool("spot", true);
            }
        }
        else if (!enemyIA.spot) animator.SetBool("spot", false);


     

        //range
        if (enemyIA.range) animator.SetBool("range", true);
        else if (!enemyIA.range) animator.SetBool("range", false);

        //attack
        if (enemyIA.attackAction) animator.SetBool("attack", true);
        else if (!enemyIA.attackAction) animator.SetBool("attack", false);


        //die
        if (enemyIA.die) animator.SetBool("die", true);

        
        if (golpe) timer-= Time.deltaTime;
        
        
        if (lifeSaved-enemyIA.life>0)
        {
            animator.SetBool("hit", true);
            golpe = true;
            lifeSaved--; 
        }
        else if (timer < 0)
        {
            animator.SetBool("hit", false);
            golpe = false;
            timer = timerGolpe;

        }
        //Debug.Log(golpe);
        

 

    }

}

