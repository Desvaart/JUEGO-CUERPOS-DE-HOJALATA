using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectEnemy : MonoBehaviour
{
    public float radius = 1.5f;
    float timer;
    public float timeAttack = 1f;
    public RigidCharacter rigidCharacter;
    public bool lookingEnemy;
    bool attack;
    bool coolDown;
    

    void Start()
    {
        
        timer = timeAttack;
        attack = true;
    }

    void Update()
    {
        // Debug.Log(timer);


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.transform.TryGetComponent(out Enemy enemy))
            {

                Vector3 point = hitCollider.transform.position - transform.position;

                if (Vector3.Dot(rigidCharacter.direction, point.normalized) > 0.5f)
                {
                    lookingEnemy = true;

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (attack)
                        {

                            hitCollider.SendMessage("Life", SendMessageOptions.DontRequireReceiver);
                            timer = timeAttack;
                            attack = false;

                        }
                    }


                }
                else lookingEnemy = false;

            }
        }

        if (!attack) 
        { 
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }

        if (timer <= 0)
        {
            attack = true;
        }

    }

    
}

