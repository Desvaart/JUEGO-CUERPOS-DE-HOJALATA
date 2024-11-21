using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animEnemy : MonoBehaviour
{
    private Animator _animator;
    public EnemyIA enemyIA;
    void Start()
    {
        _animator = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
       
       if (enemyIA.bringDown) _animator?.SetBool("isUp", false);
        else if (!enemyIA.bringDown) _animator?.SetBool("isUp", true);

       


    }
}
