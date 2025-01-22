using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyIA enemyIA;
    public animacionKungFu animK;
    public bool basicEnemy;
    public bool kungFu;
    public bool robot;

    void Start()
    {
       
    }

    public void Life()
    {
        //hit
        if (basicEnemy)
        {
            enemyIA.life--;
            //Debug.Log(enemyIA.life);
            if (enemyIA.life == 0) enemyIA.SendMessage("EndLife", SendMessageOptions.DontRequireReceiver);
        }
        else if (kungFu)
        {
            
            animK.life--;
            //Debug.Log(enemyIA.life);
            
            
        }
        else if (robot)
        {

        }
    }

        

}
