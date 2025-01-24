using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con el Canvas y las imágenes
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena



public class FallDetector : MonoBehaviour
{
    public Image lifeImage; // Referencia a la imagen que se mostrará en el Canvas
    public GameObject gameOverPanel; // Referencia al panel de Game Over
    private int currentLives; // Número actual de vidas
    public Sprite sprite0Lives; // Sprite para 0 vidas
    public GameObject gameOverText; // Referencia al texto "GAME OVER"



    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que toca el suelo es el jugador
        if (other.CompareTag("Player"))
        {
            // Mostrar la pantalla de Game Over
            ShowGameOver();
            currentLives= 0;
            lifeImage.sprite = sprite0Lives;
            StartCoroutine(RestartGameWithDelay(3f)); // Llama a la corrutina con 5 segundos de retraso
        }
    }

    private void ShowGameOver()
    {
        // Pausar el tiempo y activar la pantalla de Game Over
        Time.timeScale = 0f;
        if (gameOverPanel != null)
       {
          gameOverPanel.SetActive(true);
       }
    }

        private IEnumerator RestartGameWithDelay(float delay)
    {
        Debug.Log("Game Over. Reiniciando en " + delay + " segundos...");

        if (gameOverText != null)
        {
            gameOverText.SetActive(true); // Muestra el texto "GAME OVER"
        }

        Time.timeScale = 0f; // Congela el tiempo en el juego
        yield return new WaitForSecondsRealtime(delay); // Espera el tiempo real (ignora el Time.timeScale)

        if (gameOverText != null)
        {
            gameOverText.SetActive(false); // Oculta el texto antes de reiniciar
        }

        Time.timeScale = 1f; // Reactiva el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }
}
