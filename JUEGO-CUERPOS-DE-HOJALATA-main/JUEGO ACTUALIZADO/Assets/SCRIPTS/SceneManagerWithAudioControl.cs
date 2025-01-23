using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWithAudioControl : MonoBehaviour
{
    public AudioSource musicAudioSource; // Referencia al AudioSource de la música

    void Start()
    {
        // Verifica si el AudioSource está asignado y empieza con el volumen en 1 (máximo)
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = 1f; // Asume que comienza con volumen completo
        }
    }

    // Método que cambia la escena y reduce el volumen de la música
    public void LoadNextScene(string sceneName)
    {
        // Reducir el volumen antes de cambiar de escena
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = 0f; // Baja el volumen a 0 (silencio)
        }

        // Cargar la nueva escena
        SceneManager.LoadScene(sceneName);
    }

    // Al cambiar de escena, automáticamente bajamos el volumen del audio
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Evento que ocurre cada vez que una nueva escena es cargada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Bajar el volumen del AudioSource al cambiar a una nueva escena
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = 0f; // Baja el volumen a 0 en la nueva escena
        }
    }
}
