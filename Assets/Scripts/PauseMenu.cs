using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Panel del menú de pausa
    private bool isPaused = false;

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
        pauseMenuUI.SetActive(false); // Oculta el menú de pausa
        Time.timeScale = 1f; // Restaura el tiempo
        isPaused = false;
    }

    // Pausa el juego
    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Muestra el menú de pausa
        Time.timeScale = 0f; // Detiene el tiempo
        isPaused = true;
    }

    // Vuelve al menú principal
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Restaura el tiempo antes de cambiar de escena
        SceneManager.LoadScene("Main"); // Cambia a la escena del menú principal
    }

    // Cierra el juego
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Muestra en el editor
        Application.Quit();
    }
    public void PauseFromButton()
    {
        if (!isPaused) // Solo pausa si no está ya pausado
        {
            Pause();
        }
    }
}
