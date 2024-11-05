using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPlataformas : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = -3.0f;
    public float plataformWidth = 7.0f;
    public float bounceDistance = 0.5f;
    bool collide;
    bool ChangeDirection=true;
    public LayerMask whatIsGround;
    public bool startRight;
    private bool setUpGone;
    Vector3 Direction;

    void Setup()
    {
        
    }

  

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        
        if (ChangeDirection)
        {
            Direction = Vector3.right;
        }
        if (!ChangeDirection) 
        {
            Direction = Vector3.left;
        }
        collide = Physics.Raycast(transform.position, Direction, plataformWidth * 0.5f + bounceDistance, whatIsGround);

        if (!setUpGone)
        {
            if (startRight)
            {

                ChangeDirection = true;


            }
            else
            {
                ChangeDirection = false;
                
            }
        }


        
        
        if (collide)
        {
            setUpGone = true;
            speed *= -1;
            ChangeDirection = !ChangeDirection;
            

        }
         


    }
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            

            collision.gameObject.transform.SetParent(transform);
            


        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }

    }

}
