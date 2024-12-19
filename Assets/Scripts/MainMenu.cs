using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MainMenu : MonoBehaviour
{
    AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Funci�n para el bot�n Play
    public void PlayGame()
    {
        m_AudioSource.Play();
        // Cambia a la escena principal de juego
        // Reemplaza "GameScene" con el nombre exacto de tu escena principal
        SceneManager.LoadScene("Tarea 1");
    }

    // Funci�n para el bot�n Credits
    public void Credits()
    {
        m_AudioSource.Play();

        // Activar panel de cr�ditos

    }



    // Funci�n para el bot�n Quit
    public void QuitGame()
    {
        m_AudioSource.Play();
        // Cierra el juego
        Debug.Log("Quit Game"); // Esto funciona en el editor
        Application.Quit();
    }
}
