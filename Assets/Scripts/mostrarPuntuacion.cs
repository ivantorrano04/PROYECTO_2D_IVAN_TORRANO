using UnityEngine;
using TMPro;

public class MostrarPuntuacion : MonoBehaviour
{
    public TMP_Text TextoPuntuacion;
    public JohnMovement johnMovement; // Arrastrarás a John aquí

    void Update()
    {
        if (TextoPuntuacion != null && johnMovement != null)
        {
            TextoPuntuacion.text = "Puntos: " + johnMovement.ObtenerPuntuacion().ToString();
        }
    }
}