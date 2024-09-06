using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cotroladoragua : MonoBehaviour
{    
    
    public GameObject waterObject; // Referencia al objeto de agua
    public Transform startPoint; // Punto de inicio para el movimiento del agua
    public Transform endPoint; // Punto final para el movimiento del agua
    public float moveSpeed = 2f; // Velocidad a la que el agua se mueve
    public string playerTag = "Player"; // Tag del jugador

    private bool shouldMove = false; // Si el agua debería moverse

    void Start()
    {
        // Verificar que las referencias están asignadas
        if (waterObject == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("Una o más referencias no están asignadas en el Inspector.");
            return;
        }

        // Inicializar la posición del agua en el punto de inicio
        waterObject.transform.position = startPoint.position;
    }

    void Update()
    {
        if (shouldMove && waterObject != null)
        {
            // Determinar el objetivo para el movimiento
            Vector3 targetPosition = endPoint.position;

            // Mover el agua hacia el objetivo
            waterObject.transform.position = Vector3.MoveTowards(waterObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Detener el movimiento cuando el agua alcanza la posición objetivo
            if (Vector3.Distance(waterObject.transform.position, targetPosition) < 0.01f)
            {
                shouldMove = false; // Detener el movimiento al alcanzar el objetivo
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar que el collider activado tiene el tag del jugador
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player detected: " + other.name);
            ActivateWater();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar que el collider que sale es el jugador
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player left: " + other.name);
            DeactivateWater();
        }
    }

    private void ActivateWater()
    {
        shouldMove = true; // Iniciar el movimiento del agua
    }

    private void DeactivateWater()
    {
        shouldMove = false; // Detener el movimiento del agua
    }
}

