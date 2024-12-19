using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con el Canvas y las imágenes
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class PlayerHealthUI : MonoBehaviour
{
    public int maxLives = 3; // Número máximo de vidas
    private int currentLives; // Número actual de vidas

    public Image lifeImage; // Referencia a la imagen que se mostrará en el Canvas
    public Sprite sprite3Lives; // Sprite para 3 vidas
    public Sprite sprite2Lives; // Sprite para 2 vidas
    public Sprite sprite1Life;  // Sprite para 1 vida
    public Sprite sprite0Lives; // Sprite para 0 vidas

    public GameObject gameOverText; // Referencia al texto "GAME OVER"

    private bool isGameOver = false; // Evita múltiples llamadas a RestartGame()

    private void Start()
    {
        currentLives = maxLives; // Inicializa las vidas al máximo
        UpdateLifeImage(); // Actualiza la imagen inicial

        if (gameOverText != null)
        {
            gameOverText.SetActive(false); // Asegúrate de que el texto esté oculto al iniciar
        }
    }

    private void Update()
    {
        // Detecta si el jugador presiona la tecla "H" para bajar vida
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
        }
    }

    // Método para reducir vida al personaje
    public void TakeDamage()
    {
        if (isGameOver) return; // No hacer nada si ya es Game Over

        currentLives--; // Reduce las vidas
        if (currentLives < 0)
        {
            currentLives = 0; // No puede bajar de 0
        }

        UpdateLifeImage(); // Actualiza la imagen correspondiente

        // Verificar si las vidas llegaron a 0
        if (currentLives == 0)
        {
            isGameOver = true; // Marca el juego como terminado
            StartCoroutine(RestartGameWithDelay(5f)); // Llama a la corrutina con 5 segundos de retraso
        }
    }

    // Método para actualizar el sprite de la imagen según las vidas restantes
    private void UpdateLifeImage()
    {
        switch (currentLives)
        {
            case 3:
                lifeImage.sprite = sprite3Lives;
                break;
            case 2:
                lifeImage.sprite = sprite2Lives;
                break;
            case 1:
                lifeImage.sprite = sprite1Life;
                break;
            case 0:
                lifeImage.sprite = sprite0Lives;
                break;
            default:
                Debug.LogWarning("Estado de vida desconocido");
                break;
        }
    }

    // Corrutina para reiniciar el juego con un retraso
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




