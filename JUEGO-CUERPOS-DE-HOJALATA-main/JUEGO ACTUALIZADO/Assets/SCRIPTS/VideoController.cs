using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;    // El VideoPlayer que reproduce el video.
    public AudioSource audioSource;    // El AudioSource que reproduce el audio.

    private bool isSceneTransitionTriggered = false; // Para evitar múltiples llamadas a la transición.

    void Start()
    {
        // Comienza el video y el audio al mismo tiempo
        videoPlayer.Play();
        audioSource.Play();

        // Escuchar el evento de finalización del video
        videoPlayer.loopPointReached += CheckEndConditions;
    }

    void Update()
    {
        // Verificar si el audio ha terminado
        if (!audioSource.isPlaying && !isSceneTransitionTriggered)
        {
            CheckEndConditions(videoPlayer);
        }
    }

    // Método que verifica si ambos han terminado y realiza la transición
    private void CheckEndConditions(VideoPlayer vp)
    {
        // Asegúrate de que el video y el audio hayan terminado
        if (videoPlayer.isPlaying || audioSource.isPlaying) return;

        // Evita múltiples transiciones
        isSceneTransitionTriggered = true;

        // Cambiar a la siguiente escena
        SceneManager.LoadScene("enemy_setup"); // Cambia el nombre por el de tu siguiente escena
    }
}
