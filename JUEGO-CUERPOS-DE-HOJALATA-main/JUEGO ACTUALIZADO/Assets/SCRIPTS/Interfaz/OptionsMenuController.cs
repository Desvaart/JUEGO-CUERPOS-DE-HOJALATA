using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public Button backButton; // Bot�n de "Volver"
    public GameObject mainMenuPanel;  // Panel principal
    public GameObject optionsMenuPanel;  // Panel de opciones

    void Start()
    {
        // Asignamos la acci�n al bot�n de Volver
        backButton.onClick.AddListener(BackToMainMenu);
    }

    // M�todo para volver al men� principal
    void BackToMainMenu()
    {
        // Ocultamos el men� de opciones y mostramos el men� principal
        optionsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}

