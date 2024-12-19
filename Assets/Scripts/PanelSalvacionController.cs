using UnityEngine;
using TMPro;
using System;

public class PanelSalvacionController : MonoBehaviour
{
    public TextMeshProUGUI score, maxScore;
    public static int aliens, maxAliens;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        }
    }
}
