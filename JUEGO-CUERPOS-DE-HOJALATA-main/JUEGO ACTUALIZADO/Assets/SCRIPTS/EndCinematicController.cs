using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndCinematicController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource; // El AudioSource de la música que debe detenerse
    public string nextSceneName = "MainMenu"; // El nombre de la escena a la que irás

    void Start()
    {
        // Se asegura de que la música de la escena actual esté activa
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Reproduce la cinematica
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Función que se llama cuando el video llega al final
    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Cinemática terminada. Saliendo al juego.");

        // Detén la música antes de cambiar de escena
        if (audioSource != null)
        {
            audioSource.Stop(); // Detiene la música
        }

        // Carga la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
