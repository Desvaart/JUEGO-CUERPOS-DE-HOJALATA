using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbPoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed;
    public float distance;
    private int waypointsIndex = 0;
    bool subido;

    void Update()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, waypoints[i].transform.position) < distance * 3)
            {
                waypointsIndex=i;
                
            }
            else
            {
                waypointsIndex = 0;
            }
           
        }
        //Debug.Log(waypointsIndex);

        

        if (Vector3.Distance(transform.position, waypoints[waypointsIndex].transform.position) < distance && subido)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, waypoints[waypointsIndex].transform.position) < 0.1f) subido = false;
        if (Vector3.Distance(transform.position, waypoints[waypointsIndex].transform.position) > distance + 0.5f) subido = true;





        
               
        

        
      





    }

    


}
