using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoVida; // Arrastra aquí el objeto "fill"
    public JohnMovement johnMovement;

    private float vidaMaxima;

    void Start()
    {
        if (johnMovement == null)
        {
            Debug.LogError("¡Asigna el jugador John en el Inspector!");
            return;
        }
        vidaMaxima = johnMovement.vidaMaxima;
    }

    void Update()
    {
        if (rellenoVida != null && johnMovement != null)
        {
            // Calculamos el porcentaje de vida
            float fillPercent = johnMovement.VidaActual / vidaMaxima;

            // Escalamos el relleno horizontalmente
            // El ancho será proporcional al porcentaje de vida
            rellenoVida.rectTransform.localScale = new Vector3(fillPercent, 1f, 1f);
        }
    }
}