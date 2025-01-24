using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // M�todo para reintentar el nivel actual
    public void RetryGame()
    {
        Time.timeScale = 1f; // Restablece el tiempo normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    // M�todo para salir al men� principal
    public void ExitToMenu()
    {
        Time.timeScale = 1f; // Restablece el tiempo normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
