using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoVida;
    public JohnMovement johnMovement; // Arrastra a John aquí en el Inspector

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
            rellenoVida.fillAmount = johnMovement.VidaActual / vidaMaxima;
        }
    }
}