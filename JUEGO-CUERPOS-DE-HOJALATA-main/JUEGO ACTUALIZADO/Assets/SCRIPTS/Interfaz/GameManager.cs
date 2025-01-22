using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuPanel; // Panel del men� principal

    private bool isGameStarted = false; // Indica si el juego ha comenzado

    void Start()
    {
        // Pausar el juego al inicio
        Time.timeScale = 0;
        isGameStarted = false;

        // Asegurarse de que el men� est� visible
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        // Inicia el juego al pulsar "Jugar"
        Time.timeScale = 1;
        isGameStarted = true;

        // Ocultar el men� principal
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }
    }
}

