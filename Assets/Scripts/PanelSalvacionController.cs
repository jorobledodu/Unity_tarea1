using UnityEngine;
using TMPro;
using System;
using Gamekit3D.GameCommands;
using System.Collections;

public class PanelSalvacionController : MonoBehaviour
{
    public TextMeshProUGUI score, maxScore;
    public static int aliens, maxAliens;
    public static int numeroTotalAliens;
    int chompersSalvados = 0;
    public TextMeshProUGUI puntos;

    //Sonidito
    public AudioSource AudioSource;

    //Para el cambio de Sprites
    public GameObject Alien1;
    public GameObject Alien2;

    //EFECTO DISSOLVE:
    [SerializeField] private Material dissolveMaterial; // Material de disolución
    Material currentMaterial;
    private float dissolveSpeed = 0.05f; // Velocidad de disolución
    private Renderer[] childRenderers; // Renderers de los hijos
    private float dissolveAmount = 0f; // Valor del parámetro de disolución


    //VÓRTICE
    public GameObject vortice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numeroTotalAliens = GameObject.FindGameObjectsWithTag("alien").Length;
        Debug.Log("numero aliens: " + numeroTotalAliens);

        maxAliens = PlayerPrefs.GetInt("maxScore", 0);
        maxScore.text = "Max: " + maxAliens.ToString();

        aliens = 0;
        score.text = "Aliens saved: 0/" + numeroTotalAliens.ToString();

        
        //EFECTO DISSOLVE:
        // Obtener todos los renderers de los hijos
        childRenderers = GetComponentsInChildren<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "alien")
        {
            // salvar 3 chompers y disolverse
            chompersSalvados++;
            puntos.text = chompersSalvados.ToString() + "/3";
            if(chompersSalvados == 3)
            {
                //disolverse
                currentMaterial = new Material(dissolveMaterial);
                for (int i = 0; i < childRenderers.Length; i++)
                {                   
                    childRenderers[i].material = currentMaterial;
                }
                StartCoroutine("Dissolve");  
            }

            aliens++;
            score.text = "Aliens saved: " + aliens.ToString() + "/" + numeroTotalAliens.ToString();

            if (aliens > maxAliens)
            {
                maxAliens = aliens;
                maxScore.text = "Max: " + maxAliens.ToString();
                PlayerPrefs.SetInt("maxScore", maxAliens);
            }

            Destroy(collision.gameObject);

            //if (aliens == numeroTotalAliens)
            if (aliens == 3) //PARA PRUEBAS!!!!!!!
            {
                Debug.Log("todos los aliens salvados");

                //ACTIVAR VÓRTICE
                vortice.SetActive(true);

                
            }

            AudioSource.Play(); 

            Invoke("CambioDeSprite1", 0.01f);
            Invoke("CambioDeSprite2", 1f);

        }
    }

    private void CambioDeSprite1()
    {
        Alien1.SetActive(false);
        Alien2.SetActive(true);
    }

    private void CambioDeSprite2()
    {
        Alien1.SetActive(true);
        Alien2.SetActive(false);
    }

    IEnumerator Dissolve()
    {
        while (dissolveAmount < 1.0f)
        {
            dissolveAmount += dissolveSpeed;
            currentMaterial.SetFloat("Dissolve", dissolveAmount);
            Debug.Log("Valor del slider: " + currentMaterial.GetFloat("Dissolve"));
            yield return new WaitForSeconds(dissolveSpeed); // Espera un frame
        }

        Debug.Log("Disolución completada");
        Destroy(gameObject); 
    }
}
