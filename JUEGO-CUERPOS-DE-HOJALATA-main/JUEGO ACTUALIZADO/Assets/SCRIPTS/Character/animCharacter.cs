using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animCharacter : MonoBehaviour
{
    private Animator _animator;
    private float horizontalInput;
    private float verticalInput;
    public RigidCharacter rigidCharacter;
    public Crouch crch;
    //public detectEnemy dEnemy;

    
    
   
   


    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        HorizontalMovment();
        VerticalMovment();
        Jumping();
        Climbing();
        Crouching();
        Attack();







    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    private void HorizontalMovment()
    {
        
        if (horizontalInput == 0)
        {
            _animator?.SetBool("stopped", true);
        }
        else
        {
            _animator?.SetBool("stopped", false);

            if (horizontalInput < 0)
            {
                _animator?.SetBool("left", true);
            }
            if (horizontalInput > 0)
            {
                _animator?.SetBool("left", false);
            }
        }
    }
    private void VerticalMovment()
    {


        if (verticalInput ==0)
        {
            _animator?.SetBool("stopped_vertical", true);
        }
        else
        {
            _animator?.SetBool("stopped_vertical", false);

            if (verticalInput < 0)
            {
                _animator?.SetBool("up", false);
            }
            if (verticalInput > 0)
            {
                _animator?.SetBool("up", true);
            }
        }

    }

    private void Jumping()
    {
        if (rigidCharacter.jumping)
        {
            _animator?.SetBool("jump", true);
        }
        else
        {
            _animator?.SetBool("jump", false);
        }
    }
    private void Climbing()
    {
        if (rigidCharacter.climbing)
        {
            _animator?.SetBool("climbing", true);
        }
        else
        {
            _animator?.SetBool("climbing", false);
        }
    }
    private void Crouching()
    {
        if (crch.IsCrouched)
        {
            _animator?.SetBool("crouch", true);
            
        }
        else
        {
            _animator?.SetBool("crouch", false);
            
        }

    }
    private void Attack()
    {
        if(rigidCharacter.lookingEnemy) _animator?.SetBool("inSight", true);
        else _animator?.SetBool("inSight", false);

        if (rigidCharacter.quitarVida) _animator?.SetBool("attack", true);
        else _animator?.SetBool("attack", false);

        if (rigidCharacter.parry) _animator?.SetBool("parry", true);
        else _animator?.SetBool("parry", false);
    }




}
