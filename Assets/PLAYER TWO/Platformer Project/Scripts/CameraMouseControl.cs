using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{ 
    public float mouseSensitivity = 100f; // Sensibilidad del ratón
    private float xRotation = 0f;         // Controlar la rotación en el eje X (arriba/abajo)
    private float yRotation = 0f;         // Controlar la rotación en el eje Y (izquierda/derecha)

    void Start()
    {
        // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Obtener el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Control de la rotación vertical (arriba y abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical

        // Control de la rotación horizontal (izquierda y derecha)
        yRotation += mouseX;

        // Aplicar la rotación a la cámara (usando Quaternion para una rotación suave)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
