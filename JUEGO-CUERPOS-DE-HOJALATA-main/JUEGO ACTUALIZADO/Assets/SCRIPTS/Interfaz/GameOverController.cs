using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Método para reintentar el nivel actual
    public void RetryGame()
    {
        Time.timeScale = 1f; // Restablece el tiempo normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    // Método para salir al menú principal
    public void ExitToMenu()
    {
        Time.timeScale = 1f; // Restablece el tiempo normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
