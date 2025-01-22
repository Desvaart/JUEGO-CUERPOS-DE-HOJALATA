using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Referencia al AudioManager
    public AudioManager audioManager;

    // Método para iniciar el juego
    public void StartGame()
    {
        // Detener la música
        audioManager.StopMusic();

        // Cargar la escena de juego
        //SceneManager.LoadScene("enemy_setup");  // Asegúrate de que el nombre de tu escena sea correcto
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Application.Quit();
    }
}

