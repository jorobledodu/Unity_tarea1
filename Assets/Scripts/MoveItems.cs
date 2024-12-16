using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItems : MonoBehaviour
{
    bool haCogido = false;
    public Transform grabPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "item" || other.tag == "item_terremoto")
        {
            Debug.Log("tocando item");

            if (Input.GetMouseButton(0)  )
            {
                if (!haCogido)
                {
                    Debug.Log("cogiendo item");
                    haCogido=true;
                    other.transform.parent = grabPoint.transform;
                    other.transform.position = grabPoint.transform.position;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                }


            }
            else
            {
                if (haCogido)
                    {
                        Debug.Log("soltando item");
                        haCogido=false;
                        other.transform.parent = null;
                        other.GetComponent<Rigidbody>().isKinematic = false;
                    }
            }
            
        }
    }
}
