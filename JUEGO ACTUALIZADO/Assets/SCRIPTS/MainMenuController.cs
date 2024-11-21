using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Referencias a los botones
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;

    // Referencias a los paneles
    public GameObject mainMenuPanel;  // Panel principal
    public GameObject optionsMenuPanel;  // Panel de opciones
    public GameObject gameUIPanel;  // Panel de la interfaz de juego

    void Start()
    {
        // Inicializamos los botones
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(OpenOptions);
        exitButton.onClick.AddListener(ExitGame);

        // Inicializamos los paneles
        mainMenuPanel.SetActive(true);  // El menú principal está visible
        optionsMenuPanel.SetActive(false);  // El panel de opciones está oculto
        gameUIPanel.SetActive(false);  // La interfaz de juego está oculta
    }

    // Método para iniciar el juego
    void StartGame()
    {
        // Ocultamos el menú principal y mostramos la interfaz de juego
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);  // Aseguramos que el panel de opciones esté oculto también
        gameUIPanel.SetActive(true); // Mostrar interfaz de juego
        //SceneManager.LoadScene("MainScene"); // Cambia "MainScene" por el nombre de tu escena principal
    }

    // Método para abrir el menú de opciones
    void OpenOptions()
    {
        // Ocultamos el menú principal y mostramos el de opciones
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    // Método para salir del juego
    void ExitGame()
    {
        // Si estamos en el editor, terminamos la ejecución del juego. En una compilación final, cerramos el juego.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Método para volver al menú principal desde el menú de opciones
    public void BackToMainMenu()
    {
        optionsMenuPanel.SetActive(false);  // Ocultar el panel de opciones
        mainMenuPanel.SetActive(true);  // Mostrar el menú principal
    }
}

