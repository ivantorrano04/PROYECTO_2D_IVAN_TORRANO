using UnityEngine;

public class recargarVida : MonoBehaviour
{
    public int cantidadVida = 2; // Cuánta vida recupera

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisionó es el jugador (tag "Player")
        if (other.CompareTag("Player"))
        {
            JohnMovement john = other.GetComponent<JohnMovement>();
            if (john != null)
            {
                // Llama a un nuevo método en JohnMovement para curar
                john.Curar(cantidadVida);
                
                // Destruye el objeto de vida al recogerlo
                Destroy(gameObject);
            }
        }
    }
}