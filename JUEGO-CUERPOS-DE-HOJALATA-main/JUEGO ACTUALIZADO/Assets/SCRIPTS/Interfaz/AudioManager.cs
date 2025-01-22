using UnityEngine;
using UnityEngine.SceneManagement;  // Para manejar la carga de escenas

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {

        //audioSource = GetComponent<AudioSource>();
        // Cargar el volumen guardado o usar un valor predeterminado
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SetVolume(savedVolume);

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Comenzar a reproducir la música solo si estamos en el menú principal
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            audioSource.Play();
        }
    }

    // Función que se llama cuando se inicia el juego
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume); // Guardar el volumen
    }

    public float GetVolume()
    {
        return audioSource != null ? audioSource.volume : 0.5f;
    }

    public static AudioManager Instance; // Singleton para que sea único


    private void Awake()
    {
        // Asegurarse de que solo haya un AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

    }
}

