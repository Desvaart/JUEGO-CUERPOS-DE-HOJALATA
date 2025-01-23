using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;    // El VideoPlayer que reproduce el video.
    public AudioSource audioSource;    // El AudioSource que reproduce el audio.

    private bool isSceneTransitionTriggered = false; // Para evitar m�ltiples llamadas a la transici�n.

    void Start()
    {
        // Comienza el video y el audio al mismo tiempo
        videoPlayer.Play();
        audioSource.Play();

        // Escuchar el evento de finalizaci�n del video
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

    // M�todo que verifica si ambos han terminado y realiza la transici�n
    private void CheckEndConditions(VideoPlayer vp)
    {
        // Aseg�rate de que el video y el audio hayan terminado
        if (videoPlayer.isPlaying || audioSource.isPlaying) return;

        // Evita m�ltiples transiciones
        isSceneTransitionTriggered = true;

        // Cambiar a la siguiente escena
        SceneManager.LoadScene("enemy_setup"); // Cambia el nombre por el de tu siguiente escena
    }
}
