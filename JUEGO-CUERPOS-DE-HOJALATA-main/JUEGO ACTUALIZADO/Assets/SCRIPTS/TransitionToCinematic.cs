using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Importante para gestionar escenas

public class TransitionToCinematic : MonoBehaviour
{
    public AudioSource musicAudioSource; // El AudioSource que está reproduciendo la música
    public Slider volumeSlider;     // Referencia al Slider del volumen
    public KeyCode triggerKey = KeyCode.P; // La tecla que activará la transición
    public string cinematicSceneName = "EndCinematicScene"; // Nombre exacto de la escena donde estará la cinemática

    public int Slider { get; private set; }

    void Update()
    {
        // Detectar si se pulsa la tecla configurada
        if (Input.GetKeyDown(triggerKey))
        {
            TriggerCinematic(); // Llamamos a la función que realiza la transición

            // Bajar el volumen del audio a 0
            if (musicAudioSource != null)
            {
                Slider= 0; // Baja el volumen a 0
            }

        }
    }

    private void TriggerCinematic()
    {
        // Mensaje en consola y cambio de escena
        Debug.Log($"Tecla {triggerKey} pulsada. Transicionando a la escena de la cinemática: {cinematicSceneName}");
        SceneManager.LoadScene(cinematicSceneName); // Cambia a la escena configurada
    }
}
