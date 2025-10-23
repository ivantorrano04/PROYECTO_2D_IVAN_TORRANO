using UnityEngine;
using TMPro; // 👈 Importante para TextMeshPro

public class ContadorBalas : MonoBehaviour
{
    public TMP_Text textoBalas;      // Referencia al texto
    public JohnMovement johnMovement; // Referencia a John

    void Update()
    {
        if (textoBalas != null && johnMovement != null)
        {
            textoBalas.text = $"{johnMovement.BalasActuales}/{johnMovement.maxBalas}";
        }
    }
}