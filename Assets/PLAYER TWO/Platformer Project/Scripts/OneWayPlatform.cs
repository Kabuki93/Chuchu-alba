using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class OneWayPlatform : MonoBehaviour
{
   private Collider platformCollider; // El Collider de la plataforma
    public string playerTag = "Player"; // Tag del jugador
    public float playerThreshold = 0.1f; // Umbral para detectar si el jugador está justo debajo de la plataforma

    private GameObject player;

    void Start()
    {
        // Obtener automáticamente el Collider de la plataforma
        platformCollider = GetComponent<Collider>();

        if (platformCollider == null)
        {
           UnityEngine.Debug.Log("No se encontró un Collider en la plataforma.");
        }

        // Encontrar al jugador usando el tag asignado
        player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
        {
           UnityEngine.Debug.Log("No se encontró el jugador en la escena.");
        }
    }

    void Update()
    {
        if (player != null && platformCollider != null)
        {
            // Verifica si el jugador está por debajo de la plataforma
            if (IsPlayerBelowPlatform())
            {
                platformCollider.enabled = false; // Desactivar el colisionador para permitir el paso
            }
            else
            {
                platformCollider.enabled = true; // Activar el colisionador para impedir el paso desde arriba
            }
        }
    }

    // Método para verificar si el jugador está debajo de la plataforma
    private bool IsPlayerBelowPlatform()
    {
        // Obtener la posición Y del jugador y de la plataforma
        float playerY = player.transform.position.y;
        float platformY = transform.position.y;

        // El jugador está debajo de la plataforma si su posición Y es menor que la plataforma (con un margen de umbral)
        return playerY < platformY - playerThreshold;
    }
}
