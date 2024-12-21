using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MainMenu : MonoBehaviour
{
    AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Función para el botón Play
    public void PlayGame()
    {
        m_AudioSource.Play();
        SceneManager.LoadScene("DialogueScene");
    }

    // Función para el botón Credits
    public void Credits()
    {
        m_AudioSource.Play();

        // Activar panel de créditos

    }



    // Función para el botón Quit
    public void QuitGame()
    {
        m_AudioSource.Play();
        // Cierra el juego
        Debug.Log("Quit Game"); // Esto funciona en el editor
        Application.Quit();
    }
}
