using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public Button backButton; // Botón de "Volver"
    public GameObject mainMenuPanel;  // Panel principal
    public GameObject optionsMenuPanel;  // Panel de opciones

    void Start()
    {
        // Asignamos la acción al botón de Volver
        backButton.onClick.AddListener(BackToMainMenu);
    }

    // Método para volver al menú principal
    void BackToMainMenu()
    {
        // Ocultamos el menú de opciones y mostramos el menú principal
        optionsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}

