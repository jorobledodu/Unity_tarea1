using System.Collections;
using UnityEngine;

public class TerremotoController : MonoBehaviour
{
    GameObject[] modulos;
    float fuerzaTerremoto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        modulos = GameObject.FindGameObjectsWithTag("item_terremoto");
        Debug.Log("Elementos del terremoto: " + modulos.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Empieza terremoto!!");
            StartCoroutine("Terremoto");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Fin terremoto");
            StopCoroutine("Terremoto");
        }
        
    }
    IEnumerator Terremoto()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            
            for (int i = 0; i < modulos.Length; i++)
            {
                fuerzaTerremoto = Random.Range(1f,5f);
                Vector3 direccionFuerza = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

                modulos[i].GetComponent<Rigidbody>().AddForce(direccionFuerza * fuerzaTerremoto, ForceMode.Impulse);
            }
            //mover cámara?
        }
    }
}
