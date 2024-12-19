using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{

    CapsuleCollider PlayerCollision;
    

    float crouchHeight = 1.25f;
    float standHeight = 2f;
    float crouchOffset = 0.25f;
    bool IsCrouched;
    public RigidCharacter rigidCharacter;

    
    public KeyCode crouchKey = KeyCode.LeftShift;


    void Start()
    {
        PlayerCollision = GetComponent<CapsuleCollider>();
    }



    void Update()
    {



        if (rigidCharacter.grounded && Input.GetKeyDown(crouchKey))
        {
            Crouching();
        }

        if (IsCrouched)
        {
            if (Input.GetKeyUp(crouchKey))
                StandUp();
        }


    }



    void Crouching()
    {
        IsCrouched = true;
        PlayerCollision.height =  crouchHeight;
        PlayerCollision.center = new Vector3(0, crouchOffset, 0);
        //anim.SetTrigger("Crouch");

    }

    void StandUp()
    {
        IsCrouched = false;
        PlayerCollision.height =  standHeight;
        PlayerCollision.center = new Vector3(0, 0, 0);





    }
}
