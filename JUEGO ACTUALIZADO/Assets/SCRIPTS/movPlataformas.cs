using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPlataformas : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = -3.0f;
    private float speedSaved;
    public float plataformWidth = 7.0f;
    public float bounceDistance = 0.5f;
    bool collide;
    bool ChangeDirection=true;
    public LayerMask whatIsGround;
    public bool startRight;
    Vector3 Direction;

    void Start()
    {
        

        if (startRight)
        {

            ChangeDirection = true;
            

        }
        else
        {
            ChangeDirection = false;
            speed *=-1;
        }

        speedSaved = speed;

    }

  

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        
        if (ChangeDirection)
        {
            Direction = Vector3.right;
            speed = speedSaved;
        }
        if (!ChangeDirection) 
        {
            Direction = Vector3.left;
            speed = speedSaved; 
        }
        collide = Physics.Raycast(transform.position, Direction, plataformWidth * 0.5f + bounceDistance, whatIsGround);

    


        
        
        if (collide)
        {
            // por si interesa parar plataforma cuando llega a punto
            speed = 0;

            speedSaved *= -1;
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
