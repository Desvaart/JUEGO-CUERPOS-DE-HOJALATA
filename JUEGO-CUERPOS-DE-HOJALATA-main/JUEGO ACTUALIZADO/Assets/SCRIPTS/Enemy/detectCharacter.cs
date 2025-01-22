using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectCharacter : MonoBehaviour
{
    public bool cargado;
    public RigidCharacter rigidCharacter;

    // Start is called before the first frame update
    void Start()
    {
        cargado = true;
    }

   
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
        

            if (cargado)
            {
                rigidCharacter.SendMessage("LifeCharacter", SendMessageOptions.DontRequireReceiver);
                cargado = false;
            }



        }
    }


}
