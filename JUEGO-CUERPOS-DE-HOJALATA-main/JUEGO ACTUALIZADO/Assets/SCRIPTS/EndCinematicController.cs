using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndCinematicController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource; // El AudioSource de la m�sica que debe detenerse
    public string nextSceneName = "MainMenu"; // El nombre de la escena a la que ir�s

    void Start()
    {
        // Se asegura de que la m�sica de la escena actual est� activa
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Reproduce la cinematica
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Funci�n que se llama cuando el video llega al final
    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Cinem�tica terminada. Saliendo al juego.");

        // Det�n la m�sica antes de cambiar de escena
        if (audioSource != null)
        {
            audioSource.Stop(); // Detiene la m�sica
        }

        // Carga la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
