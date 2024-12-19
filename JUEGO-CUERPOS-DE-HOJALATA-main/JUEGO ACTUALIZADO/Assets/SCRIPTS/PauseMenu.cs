using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para reiniciar la escena y cambiar de escenas

public class PauseMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject[] gamePanels; // Una lista de todos los paneles del juego.
    public GameObject pauseMenuPanel; // Panel del menú de pausa
    public GameObject pauseOptionsPanel; // Panel de opciones del menú de pausa
    public GameObject mainMenuPanel; // Panel del menú principal
    public string mainMenuSceneName = "MainMenu"; // Nombre de la escena del menú principal

    private bool isPaused = false; // Controla si el juego está en pausa

    void Update()
    {
        // Detecta si el jugador presiona la tecla de pausa (Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void Start()
    {
        // Configurar el slider con el volumen actual
        if (volumeSlider != null)
        {
            float savedVolume = AudioManager.Instance.GetVolume();
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume); // Ajustar el volumen global
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Pausa el tiempo
        isPaused = true;
        pauseMenuPanel.SetActive(true); // Activa el menú de pausa
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Reanuda el tiempo
        isPaused = false;
        pauseMenuPanel.SetActive(false); // Desactiva el menú de pausa
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo si estaba pausado.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual.
    }


    public void OpenPauseOptionsMenu()
    {
        pauseMenuPanel.SetActive(false); // Oculta el menú de pausa
        pauseOptionsPanel.SetActive(true); // Activa el menú de opciones del menú de pausa
    }

    public void ClosePauseOptionsMenu()
    {
        pauseOptionsPanel.SetActive(false); // Oculta el menú de opciones del menú de pausa
        pauseMenuPanel.SetActive(true); // Vuelve al menú de pausa
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reanuda el tiempo si estaba pausado.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual.
    }


    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el editor
#else
        Application.Quit(); // Solo funciona en build
#endif
    }

}

