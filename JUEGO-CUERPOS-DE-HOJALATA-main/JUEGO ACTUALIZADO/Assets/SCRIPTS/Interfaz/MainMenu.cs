using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Referencia al AudioManager
    public AudioManager audioManager;

    // M�todo para iniciar el juego
    public void StartGame()
    {
        // Detener la m�sica
        audioManager.StopMusic();

        // Cargar la escena de juego
        //SceneManager.LoadScene("enemy_setup");  // Aseg�rate de que el nombre de tu escena sea correcto
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Application.Quit();
    }
}

