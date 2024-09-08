using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class OneWayPlatform : MonoBehaviour
{
    public Collider platformCollider; // El Collider de la plataforma

    private void Start()
    {
        // Asegurarse de que el collider de la plataforma esté asignado
        if (platformCollider == null)
        {
            platformCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el Collider del jugador
            Collider playerCollider = other.GetComponent<Collider>();

            // Si el jugador está por encima de la plataforma, asegurar que las colisiones están activas
            if (other.transform.position.y > transform.position.y)
            {
                Physics.IgnoreCollision(platformCollider, playerCollider, false); // Activar colisión
            }
            // Si el jugador está por debajo de la plataforma, desactivar la colisión para permitir paso
            else if (other.transform.position.y < transform.position.y)
            {
                Physics.IgnoreCollision(platformCollider, playerCollider, true); // Desactivar colisión
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el Collider del jugador
            Collider playerCollider = other.GetComponent<Collider>();

            // Restablecer la colisión cuando el jugador sale del Trigger
            Physics.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }

}
