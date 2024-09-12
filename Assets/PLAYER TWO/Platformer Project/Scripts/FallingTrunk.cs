using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrunk : MonoBehaviour
{   
    public float fallDuration = 1f;   // Duración de la caída
    public float fallAngle = 90f;     // Ángulo de caída (90 grados para una caída completa)

    private bool isFalling = false;   // Bandera para controlar si el tronco ya está cayendo
    private bool playerInTrigger = false; // Bandera para controlar si el jugador está dentro del Trigger
    private Quaternion initialLocalRotation;
    private Quaternion targetLocalRotation;

    void Start()
    {
        // Guardamos la rotación inicial del tronco en su espacio local
        initialLocalRotation = transform.localRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        // Detectar si el objeto que entra al trigger tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Detectar cuando el jugador sale del trigger
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    void Update()
    {
        // Solo caerá si el jugador está dentro del trigger, no está cayendo ya, y se ha presionado el clic izquierdo
        if (playerInTrigger && !isFalling && Input.GetMouseButtonDown(0))
        {
            // Activar la caída
            isFalling = true;

            // Determinamos la dirección de la caída en el eje X local
            if (transform.InverseTransformPoint(GameObject.FindWithTag("Player").transform.position).x > 0)
            {
                // Caída hacia la derecha en el eje X local
                targetLocalRotation = Quaternion.Euler(initialLocalRotation.eulerAngles.x + fallAngle, initialLocalRotation.eulerAngles.y, initialLocalRotation.eulerAngles.z);
            }
            else
            {
                // Caída hacia la izquierda en el eje X local
                targetLocalRotation = Quaternion.Euler(initialLocalRotation.eulerAngles.x - fallAngle, initialLocalRotation.eulerAngles.y, initialLocalRotation.eulerAngles.z);
            }

            // Iniciar la coroutine para la rotación
            StartCoroutine(RotateTrunk());
        }
    }

    private IEnumerator RotateTrunk()
    {
        float elapsedTime = 0f;

        // Rotar desde la rotación inicial hacia la rotación objetivo durante `fallDuration`
        while (elapsedTime < fallDuration)
        {
            // Interpolación en el espacio local
            transform.localRotation = Quaternion.Slerp(initialLocalRotation, targetLocalRotation, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Aseguramos que la rotación final sea exacta
        transform.localRotation = targetLocalRotation;
    }
}