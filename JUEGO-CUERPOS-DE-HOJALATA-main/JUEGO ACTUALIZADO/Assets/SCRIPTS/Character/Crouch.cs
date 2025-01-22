using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{

    CapsuleCollider PlayerCollision;
    

    public float crouchHeight = 1.25f;
    float standHeight = 2f;
    public float crouchOffset = 1.25f;
    public bool IsCrouched;
    public RigidCharacter rigidCharacter;
    bool top;
    bool next;
    public LayerMask whatIsGround;


    public KeyCode crouchKey = KeyCode.LeftShift;


    void Start()
    {
        PlayerCollision = GetComponent<CapsuleCollider>();
    }



    void Update()
    {

        WallCheck();

        if (rigidCharacter.grounded && Input.GetKeyDown(crouchKey))
        {
            Crouching();
        }

        if (IsCrouched && Input.GetKeyUp(crouchKey))
        {
            if (!top)
            {
                StandUp();
            }
            else 
            {
                next = true;
            }

        }
        if (!top && next) { StandUp(); }
        


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
        next = false;
        PlayerCollision.height =  standHeight;
        PlayerCollision.center = new Vector3(0, 1f, 0);





    }
    private void WallCheck()
    {


        top = Physics.SphereCast(transform.position, 0.2f, Vector3.up, out RaycastHit hit, 2f * 0.5f + 0.4f, whatIsGround);



    }
}
