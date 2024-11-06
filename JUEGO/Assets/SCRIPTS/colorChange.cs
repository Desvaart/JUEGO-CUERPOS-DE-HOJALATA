using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    Renderer m_Renderer;
    public bool encendido;
    public verticalPlataform verPlat;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        if (!encendido) 
        { 
            m_Renderer.material.color = Color.red;
            verPlat.green = false;
        }
        else 
        { 
            m_Renderer.material.color = Color.green;
            verPlat.green = true;
        }
    }

    public void Operate()
    {
        Debug.Log("Holaaa");
        encendido =!encendido;
        


    }

}
