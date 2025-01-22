using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjusteGrua : MonoBehaviour
{
    // Start is called before the first frame update
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
