using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class operateObjects : MonoBehaviour
{
    // Start is called before the first frame update


    public float radius = 1.5f;
    public RigidCharacter rigidCharacter;
    public KeyCode sensorKey = KeyCode.LeftControl;

    void Update()
    {
        if (Input.GetKeyDown(sensorKey))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 point = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(rigidCharacter.direction, point.normalized) > 0.5f)
                {

                    
                    
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
               
                    
                
            }
        }
    }
}
