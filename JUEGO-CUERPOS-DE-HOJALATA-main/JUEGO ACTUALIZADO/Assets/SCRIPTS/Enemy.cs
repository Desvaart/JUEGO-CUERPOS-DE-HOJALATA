using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public EnemyIA enemyIA;

 
    
    public void Life()
    {
     enemyIA.life--;
        Debug.Log(enemyIA.life);
        if (enemyIA.life == 0) enemyIA.SendMessage("EndLife", SendMessageOptions.DontRequireReceiver);
    }

    

}

