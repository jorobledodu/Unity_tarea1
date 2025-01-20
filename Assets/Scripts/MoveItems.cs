using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

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
        if (other.tag == "item" || other.tag == "item_terremoto" || other.tag == "alien" || other.tag == "alien_agente")
        {
            BehaviorGraphAgent behaviorGraphAgent = other.GetComponent<BehaviorGraphAgent>();
            NavMeshAgent navMeshAgent = other.GetComponent<NavMeshAgent>();
            RangeDetector rangeDetector = other.GetComponent<RangeDetector>();

            Debug.Log("tocando item");

            if (Input.GetMouseButton(0))
            {
                if (!haCogido)
                {
                    if (other.tag == "alien_agente")
                    {
                        behaviorGraphAgent.enabled = false;
                        navMeshAgent.enabled = false;
                        rangeDetector.enabled = false;
                    }

                    Debug.Log("cogiendo item");
                    haCogido = true;
                    other.transform.parent = grabPoint.transform;
                    other.transform.position = grabPoint.transform.position;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                }

            }
            else
            {
                if (haCogido)
                {
                    if (other.tag == "alien_agente")
                    {
                        behaviorGraphAgent.SetVariableValue("IsSafe", true);

                        behaviorGraphAgent.enabled = true;
                        navMeshAgent.enabled = true;
                        rangeDetector.enabled = true;
                    }

                    Debug.Log("soltando item");
                    haCogido = false;
                    other.transform.parent = null;
                    other.GetComponent<Rigidbody>().isKinematic = false;
                }
            }

        }
    }
}
