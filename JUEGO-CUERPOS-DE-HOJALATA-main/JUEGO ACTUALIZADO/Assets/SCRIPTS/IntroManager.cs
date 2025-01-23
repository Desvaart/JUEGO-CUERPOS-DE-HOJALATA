using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // El Video Player que reproduce la cinemática.
    public string mainMenuSceneName = "enemy_setup"; // Nombre de la escena del menú principal.

    void Start()
    {
        // Asegúrate de que el Video Player está asignado.
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Configura un evento para detectar cuando el video termina.
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // Permitir saltar la cinemática al presionar la barra espaciadora.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipIntro();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Carga la escena del menú principal cuando el video termina.
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void SkipIntro()
    {
        // Salta la cinemática y carga el menú principal.
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
