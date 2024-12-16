using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MainMenu : MonoBehaviour
{
    // Función para el botón Play
    public void PlayGame()
    {
        // Cambia a la escena principal de juego
        // Reemplaza "GameScene" con el nombre exacto de tu escena principal
        SceneManager.LoadScene("Tarea 1");
    }

    // Función para el botón Quit
    public void QuitGame()
    {
        // Cierra el juego
        Debug.Log("Quit Game"); // Esto funciona en el editor
        Application.Quit();
    }
}
