using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que toca el suelo tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Aqu� puedes implementar la l�gica para mostrar la pantalla de Game Over
            Debug.Log("El jugador ha ca�do al vac�o.");
            GameOver();
        }
    }

    private void GameOver()
    {
        // L�gica para la pantalla de Game Over (puedes adaptarla a tu proyecto)
        Time.timeScale = 0f; // Pausa el juego
        Debug.Log("�Game Over! Muestra la pantalla de fin de juego.");
        // Aqu� podr�as activar un panel o recargar la escena.
    }
}
