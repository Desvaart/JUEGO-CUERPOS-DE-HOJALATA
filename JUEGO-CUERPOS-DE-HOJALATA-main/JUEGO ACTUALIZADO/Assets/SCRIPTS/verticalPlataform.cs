using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalPlataform : MonoBehaviour
{
    public GameObject[] positions;

    public float plataformSpeed = 2;

    private float savedSpeed;

    private int positionIndex = 0;
    
    bool arrancado;

    bool arriba;

    public bool green;



    void Start()
    {
        savedSpeed = plataformSpeed;

    }


    void Update()
    {
        MovePlataform();
        
    }

    void MovePlataform()
    {
        if (green) positionIndex = 1;
        else
        {
            positionIndex = 0;
            
        }

        if (arriba && !green) arrancado = true;

        if (arrancado) plataformSpeed = savedSpeed;

        if (Vector3.Distance(transform.position, positions[positionIndex].transform.position) > 0.3f) arrancado = false;

        if (Vector3.Distance(transform.position, positions[positionIndex].transform.position) < 0.2f && !arrancado)
        {
            plataformSpeed = 0;
            if (green) arriba = true;
            else arriba = false;

        }

        transform.position = Vector3.MoveTowards(transform.position, positions[positionIndex].transform.position, plataformSpeed*Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //green=true;
            arrancado = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            arrancado = true;
            
        }
    }



}
