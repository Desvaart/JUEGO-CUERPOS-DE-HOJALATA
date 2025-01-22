using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalPlataform : MonoBehaviour
{
    public GameObject[] positions;

    public float plataformSpeed = 2;

    private float savedSpeed;

    private int positionIndex = 0;

    public bool arrancado = true;

    bool montado;

    public bool green;
    bool bucle;


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
        if(positions.Length > 2)
        {
            if (!bucle)
            {
                if (green && !montado) positionIndex = 1;
                if (green && montado) positionIndex = 2;
                if (!green && !montado) positionIndex = 0;
            }
            
            
            
        }

        if (positions.Length == 2)
        {
            
            if (green && montado) positionIndex = 1;
            else positionIndex = 0;
            
           
        }


            
        
            
        
        

        

        if (arrancado) plataformSpeed = savedSpeed;

        if (Vector3.Distance(transform.position, positions[positionIndex].transform.position) > 0.3f) arrancado = false;

        if (Vector3.Distance(transform.position, positions[positionIndex].transform.position) < 0.2f && !arrancado)
        {
            if (positionIndex == 2)
            {
                bucle = true;
                arrancado = true;
            }

            if (!bucle)
            {
                plataformSpeed = 0;
            }
            else
            {
                if (positionIndex == 2) positionIndex = 3;
                else positionIndex = 2;
            }

           

        }

        transform.position = Vector3.MoveTowards(transform.position, positions[positionIndex].transform.position, plataformSpeed*Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            arrancado = true;
            montado = true;
            
        }
    }
 
    }
