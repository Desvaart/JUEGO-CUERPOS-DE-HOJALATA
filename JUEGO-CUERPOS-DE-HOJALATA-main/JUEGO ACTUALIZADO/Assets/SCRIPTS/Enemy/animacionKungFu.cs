using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacionKungFu : MonoBehaviour
{
    private Animator otroAnimator;
    private Animator sisterAnimator;
    private Animator animator;
    public KungFu kungFu;
    bool start;
    public float life;
    float lifeSaved;
    float timer;
    float timer2;
    public float coolDown = 2f;
    public float timerGolpe = 2f;
    bool arranca;
    bool golpe;
    bool die;
    bool conversation1;
    bool conversation2;
    bool conversation3;
    public GameObject collider;
    public detectCharacter detectC;
    private RigidCharacter rigidChar;

    void Start()
    {
        otroAnimator = GameObject.Find("puerta").GetComponent<Animator>();
        sisterAnimator = GameObject.Find("sister").GetComponent<Animator>();
        rigidChar = GameObject.Find("CHARACTER").GetComponent<RigidCharacter>();
        animator = GetComponent<Animator>();
        lifeSaved = life;
        timer = timerGolpe;
        timer2 = coolDown;
    }

    
    void Update()
    {
        

        if ( kungFu.iniciado && !start)
        {
            animator.SetBool("enter", true);
            
            start = true;
        }
        else if (start)
        {
            animator.SetBool("enter", false);
            if (!conversation1)
            {
                sisterAnimator.SetBool("talk", true);
                conversation1 = true;
            }
            else sisterAnimator.SetBool("talk", false);

        }


        if (golpe) timer -= Time.deltaTime;
        if (lifeSaved - life > 0)
        {
            animator.SetBool("attacked", true);
            golpe = true;
            arranca = true;
            lifeSaved--;
        }
        else if (timer < 0)
        {
            animator.SetBool("attacked", false);
            golpe = false;
            timer = timerGolpe;

        }
        if (arranca)
        {
            detectC.cargado = true;

            if (!conversation2)
            {
                sisterAnimator.SetBool("talk", true);
                conversation2 = true;
            } else sisterAnimator.SetBool("talk", false);

            if (timer2 > 0)
            {
                animator.SetBool("attack", false);
                timer2 -=Time.deltaTime;
            }
            else
            {
                animator.SetBool("attack", true);
                timer2 = coolDown;
            }

          
        }





        if (life<=0 && rigidChar.parryTutorial) die = true;

        if (die)
        {
            if (!conversation3)
            {
                sisterAnimator.SetBool("talk", true);
                conversation3 = true;
            }
            else sisterAnimator.SetBool("talk", false);

            StartCoroutine(Dying());
            detectC.cargado = false;
            animator.SetBool("complete", true);
            otroAnimator.SetBool("completo", true);
        }

    }
    private IEnumerator Dying()
    {
        
        yield return new WaitForSeconds(1f);
        Destroy(collider);
    }


}
