using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Panel del menú de pausa
    private bool isPaused = false;
    AudioSource m_AudioSource;
    public GameObject panelTutorial;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detecta si se pulsa la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Reanuda el juego
    public void Resume()
    {
        m_AudioSource.Play();
        pauseMenuUI.SetActive(false); // Oculta el menú de pausa
        Time.timeScale = 1f; // Restaura el tiempo
        isPaused = false;
    }

    // Pausa el juego
    public void Pause()
    {
        m_AudioSource.Play();
        pauseMenuUI.SetActive(true); // Muestra el menú de pausa
        panelTutorial.SetActive(false);
        Time.timeScale = 0f; // Detiene el tiempo
        isPaused = true;
    }

    // Vuelve al menú principal
    public void LoadMainMenu()
    {
        m_AudioSource.Play();
        Time.timeScale = 1f; // Restaura el tiempo antes de cambiar de escena
        SceneManager.LoadScene("Main"); // Cambia a la escena del menú principal
    }

    // Cierra el juego
    public void QuitGame()
    {
        m_AudioSource.Play();
        Debug.Log("Quit Game"); // Muestra en el editor
        Application.Quit();
    }
    public void PauseFromButton()
    {
        m_AudioSource.Play();
        if (!isPaused) // Solo pausa si no está ya pausado
        {
            Pause();
        }
    }
}
