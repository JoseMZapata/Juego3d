using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // Personaje al que sigue la cámara
    public float sensitivity = 300f;  // Sensibilidad del mouse
    public float distance = 5f;  // Distancia de la cámara al jugador
    public float minDistance = 1.5f;  // Distancia mínima (para evitar atravesar el suelo)
    public float maxDistance = 5f;  // Distancia máxima
    public Vector2 rotationLimits = new Vector2(-40, 80);  // Límites de rotación vertical
    public LayerMask collisionLayers;  // Capas con las que la cámara puede chocar

    private float yaw = 0f;  // Rotación horizontal
    private float pitch = 0f;  // Rotación vertical

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Obtener el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, rotationLimits.x, rotationLimits.y);

        // Rotación basada en el mouse
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Comprobar colisiones con Raycast
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * maxDistance);
        RaycastHit hit;

        if (Physics.Raycast(target.position, (desiredPosition - target.position).normalized, out hit, maxDistance, collisionLayers))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);  // Ajustar la distancia según la colisión
        }
        else
        {
            distance = maxDistance;
        }

        // Aplicar la nueva posición de la cámara
        transform.position = target.position - (rotation * Vector3.forward * distance);
        transform.LookAt(target);
    }
}
