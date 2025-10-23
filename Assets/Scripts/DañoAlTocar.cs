using UnityEngine;

public class DañoAlTocar : MonoBehaviour
{
    public int daño = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("¡Colisión detectada con: " + other.name + "!");

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Es el jugador! Aplicando daño...");
            JohnMovement john = other.GetComponent<JohnMovement>();
            if (john != null)
            {
                john.Hit();
            }
            else
            {
                Debug.LogError("¡El jugador no tiene JohnMovement!");
            }
        }
    }
}