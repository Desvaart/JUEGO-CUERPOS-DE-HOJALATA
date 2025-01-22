using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectEnemies : MonoBehaviour
{
    public bool cargado;
    
    

    // Start is called before the first frame update
    void Start()
    {
        cargado = true;
    }


    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.transform.TryGetComponent(out Enemy enemy))
    {


            if (cargado)
            {
                enemy.SendMessage("Life", SendMessageOptions.DontRequireReceiver);
                cargado = false;
                
            }
            


        }
    }


}
