using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PanelSalvacionController : MonoBehaviour
{
    public TextMeshProUGUI score, maxScore;
    public static int aliens, maxAliens;
    public static int numeroTotalAliens;

    public GameObject Alien1;
    public GameObject Alien2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numeroTotalAliens = GameObject.FindGameObjectsWithTag("alien").Length;
        Debug.Log("numero aliens: " + numeroTotalAliens);

        maxAliens = PlayerPrefs.GetInt("maxScore", 0);
        maxScore.text = "Max: " + maxAliens.ToString();

        aliens = 0;
        score.text = "Aliens saved: 0/" + numeroTotalAliens.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "alien")
        {

            aliens++;
            score.text = "Aliens saved: " + aliens.ToString() + "/" + numeroTotalAliens.ToString();

            if (aliens > maxAliens)
            {
                maxAliens = aliens;
                maxScore.text = "Max: " + maxAliens.ToString();
                PlayerPrefs.SetInt("maxScore", maxAliens);
            }

            Destroy(collision.gameObject);

            if (aliens == numeroTotalAliens)
            {
                Debug.Log("todos los aliens salvados");
                SceneManager.LoadScene("Win");
            }

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
}
