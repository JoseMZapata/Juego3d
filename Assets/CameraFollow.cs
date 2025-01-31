using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al jugador
    public Vector3 offset = new Vector3(0, 3, -5); // Distancia de la cámara

    public float smoothSpeed = 5f; // Velocidad de suavizado

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada
            Vector3 desiredPosition = target.position + offset;

            // Suaviza el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Aplica la nueva posición a la cámara
            transform.position = smoothedPosition;

            // Opcional: Hacer que la cámara mire al jugador
            transform.LookAt(target);
        }
    }
}

