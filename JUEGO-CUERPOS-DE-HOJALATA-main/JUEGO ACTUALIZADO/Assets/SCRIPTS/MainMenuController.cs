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
    public AudioSource audioSource; // Referencia al AudioSource de la m�sica
    public Slider volumeSlider;     // Referencia al Slider del volumen
    public AudioClip menuMusic;      // Clip de la m�sica del men�
    public AudioClip gameMusic;      // Clip de la m�sica del juego

    void Start()
    {

        // Inicializar la m�sica en el men� principal
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // Inicializar el Slider con el volumen actual si est� asignado
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // Volumen por defecto 0.5
            audioSource.volume = savedVolume; // Aplicar el volumen guardado
            volumeSlider.value = savedVolume; // Configurar el valor inicial del Slider
            volumeSlider.onValueChanged.AddListener(SetVolume); // Escucha los cambios del Slider
        }

        Time.timeScale = 0f; // Asegurarnos de que el tiempo est� activo


        // Configurar el AudioSource para el men�
        if (audioSource != null)
        {
            audioSource.clip = menuMusic; // Asignar la m�sica del men�
            audioSource.loop = true;     // Reproducir en bucle
            audioSource.Play();          // Reproducir m�sica del men�
        }

        // Inicializamos los botones
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(OpenOptions);
        exitButton.onClick.AddListener(ExitGame);

        // Inicializamos los paneles
        mainMenuPanel.SetActive(true);  // El men� principal est� visible
        optionsMenuPanel.SetActive(false);  // El panel de opciones est� oculto
        gameUIPanel.SetActive(false);  // La interfaz de juego est� oculta
    }



    // M�todo para iniciar el juego
    void StartGame()

    {
        // Ocultamos el men� principal y mostramos la interfaz de juego
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);  // Aseguramos que el panel de opciones est� oculto tambi�n
        gameUIPanel.SetActive(true); // Mostrar interfaz de juego
                                     //SceneManager.LoadScene("MainScene"); // Cambia "MainScene" por el nombre de tu escena principal

        if (audioSource != null)
        {
            audioSource.Stop(); // Detener la m�sica del men�
            audioSource.clip = gameMusic; // Asignar la m�sica del juego
            audioSource.loop = true; // Reproducir en bucle
            audioSource.Play(); // Reproducir m�sica del juego
        }
        SceneManager.LoadScene("GameScene");

        Time.timeScale = 1f; // Asegurarnos de que el tiempo est� activo
    }

    // M�todo para abrir el men� de opciones
    void OpenOptions()
    {
        // Ocultamos el men� principal y mostramos el de opciones
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    // M�todo para salir del juego
    void ExitGame()
    {
        // Si estamos en el editor, terminamos la ejecuci�n del juego. En una compilaci�n final, cerramos el juego.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // M�todo para volver al men� principal desde el men� de opciones
    public void BackToMainMenu()
    {
        optionsMenuPanel.SetActive(false);  // Ocultar el panel de opciones
        mainMenuPanel.SetActive(true);  // Mostrar el men� principal
    }

    private void SetVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume); // Ajustar el volumen global

        // Cambiar el volumen del AudioSource seg�n el valor del Slider
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume); // Guardar el volumen ajustado
        PlayerPrefs.Save();
    }
}

