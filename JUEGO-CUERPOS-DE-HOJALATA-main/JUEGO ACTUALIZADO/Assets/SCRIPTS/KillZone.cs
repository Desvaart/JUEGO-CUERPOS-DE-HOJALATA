using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que toca el suelo tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Aquí puedes implementar la lógica para mostrar la pantalla de Game Over
            Debug.Log("El jugador ha caído al vacío.");
            GameOver();
        }
    }

    private void GameOver()
    {
        // Lógica para la pantalla de Game Over (puedes adaptarla a tu proyecto)
        Time.timeScale = 0f; // Pausa el juego
        Debug.Log("¡Game Over! Muestra la pantalla de fin de juego.");
        // Aquí podrías activar un panel o recargar la escena.
    }
}
