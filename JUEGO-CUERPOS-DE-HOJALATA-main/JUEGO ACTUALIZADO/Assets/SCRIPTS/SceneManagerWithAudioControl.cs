using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWithAudioControl : MonoBehaviour
{
    public AudioSource musicAudioSource; // Referencia al AudioSource de la m�sica

    void Start()
    {
        // Verifica si el AudioSource est� asignado y empieza con el volumen en 1 (m�ximo)
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = 1f; // Asume que comienza con volumen completo
        }
    }

    // M�todo que cambia la escena y reduce el volumen de la m�sica
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

    // Al cambiar de escena, autom�ticamente bajamos el volumen del audio
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
