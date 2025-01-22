using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KungFu : MonoBehaviour
{
    public RigidCharacter rigidCharacter;
    public bool iniciado;
    new BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {

            iniciado = true;
            Destroy(collider);

        }
    }
}
