using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // El Video Player que reproduce la cinem�tica.
    public string mainMenuSceneName = "enemy_setup"; // Nombre de la escena del men� principal.

    void Start()
    {
        // Aseg�rate de que el Video Player est� asignado.
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Configura un evento para detectar cuando el video termina.
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // Permitir saltar la cinem�tica al presionar la barra espaciadora.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipIntro();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Carga la escena del men� principal cuando el video termina.
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void SkipIntro()
    {
        // Salta la cinem�tica y carga el men� principal.
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
