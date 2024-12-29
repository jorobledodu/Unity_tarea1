using UnityEngine;

public class InstanciarPlataformas : MonoBehaviour
{
    public GameObject plataforma;
    public Transform puntoOrigen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            Destroy(other.gameObject);
            Instantiate(plataforma, puntoOrigen.position, plataforma.transform.rotation);
        }

        if (other.GetComponent<CharacterController>() != null)
        {
            Debug.Log("Keni al agua");
            other.transform.localPosition = puntoOrigen.position;
        }
    }
}
