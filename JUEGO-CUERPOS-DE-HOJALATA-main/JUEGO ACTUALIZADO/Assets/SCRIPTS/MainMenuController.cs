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

    //Referencia a la musica
    public AudioSource audioSource; // Referencia al AudioSource de la música
    public Slider volumeSlider;     // Referencia al Slider del volumen
    public AudioClip menuMusic;      // Clip de la música del menú
    public AudioClip gameMusic;      // Clip de la música del juego

    void Start()
    {

        // Inicializar la música en el menú principal
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // Inicializar el Slider con el volumen actual si está asignado
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // Volumen por defecto 0.5
            audioSource.volume = savedVolume; // Aplicar el volumen guardado
            volumeSlider.value = savedVolume; // Configurar el valor inicial del Slider
            volumeSlider.onValueChanged.AddListener(SetVolume); // Escucha los cambios del Slider
        }

        Time.timeScale = 0f; // Asegurarnos de que el tiempo esté activo


        // Configurar el AudioSource para el menú
        if (audioSource != null)
        {
            audioSource.clip = menuMusic; // Asignar la música del menú
            audioSource.loop = true;     // Reproducir en bucle
            audioSource.Play();          // Reproducir música del menú
        }

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

        if (audioSource != null)
        {
            audioSource.Stop(); // Detener la música del menú
            audioSource.clip = gameMusic; // Asignar la música del juego
            audioSource.loop = true; // Reproducir en bucle
            audioSource.Play(); // Reproducir música del juego
        }
        SceneManager.LoadScene("GameScene");

        Time.timeScale = 1f; // Asegurarnos de que el tiempo esté activo
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

    private void SetVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume); // Ajustar el volumen global

        // Cambiar el volumen del AudioSource según el valor del Slider
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume); // Guardar el volumen ajustado
        PlayerPrefs.Save();
    }
}

