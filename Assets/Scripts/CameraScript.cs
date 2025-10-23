using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform john;
    public float smoothTimeX = 0.15f; // Seguimiento horizontal rápido
    public float smoothTimeY = 0.25f; // Seguimiento vertical un poco más lento para amortiguar saltos

    private Vector2 currentVelocity;

    void LateUpdate()
    {
        if (john == null) return;

        Vector3 targetPosition = john.position;
        targetPosition.z = transform.position.z; // Mantener profundidad de la cámara

        // Separar suavizado en X e Y
        float smoothedX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref currentVelocity.x, smoothTimeX);
        float smoothedY = Mathf.SmoothDamp(transform.position.y, targetPosition.y, ref currentVelocity.y, smoothTimeY);

        transform.position = new Vector3(smoothedX, smoothedY, transform.position.z);
    }
}   