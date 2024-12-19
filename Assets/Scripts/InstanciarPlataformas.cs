using UnityEngine;

public class InstanciarPlataformas : MonoBehaviour
{
    public GameObject plataforma;
    public Transform puntoOrigen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            Destroy(other.gameObject);
            Instantiate(plataforma, puntoOrigen.position, plataforma.transform.rotation);
        }
    }
}
