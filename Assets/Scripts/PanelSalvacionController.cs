using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PanelSalvacionController : MonoBehaviour
{
    public TextMeshProUGUI score, maxScore;
    public static int aliens, maxAliens;
    public static int numeroTotalAliens;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numeroTotalAliens = GameObject.FindGameObjectsWithTag("alien").Length;
        Debug.Log("numero aliens: " +  numeroTotalAliens);

        maxAliens = PlayerPrefs.GetInt("maxScore", 0);
        maxScore.text = "Max: " + maxAliens.ToString();

        aliens = 0;
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
            score.text = "Aliens saved: " + aliens.ToString();

            
            if(aliens > maxAliens)
            {
                maxAliens = aliens;
                maxScore.text = "Max: " + maxAliens.ToString();
                PlayerPrefs.SetInt("maxScore", maxAliens);
            }

            Destroy(collision.gameObject);

            if(aliens == numeroTotalAliens) 
            {
                Debug.Log("todos los aliens salvados");
                SceneManager.LoadScene("Win");
            }

        }
    }
}
