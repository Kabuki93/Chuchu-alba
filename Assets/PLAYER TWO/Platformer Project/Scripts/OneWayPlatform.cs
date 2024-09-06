using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class OneWayPlatform : MonoBehaviour
{
    public Collider platformCollider;  // El collider de la plataforma
    public GameObject player;         // Referencia al jugador

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Encuentra al jugador
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger es el jugador
        if (other.CompareTag("Player"))
        {
           
        player = GameObject.FindGameObjectWithTag("Player");  // Encuentra al jugador
        if (platformCollider == null)
        {
            UnityEngine.Debug.Log("El collider de la plataforma no está asignado.");
        }
        if (player == null)
        {
            UnityEngine.Debug.Log("No se pudo encontrar el objeto con el tag 'Player'.");
        }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador sale del trigger
        if (other.CompareTag("Player"))
        {
           UnityEngine.Debug.Log("El jugador ha salido del trigger.");
            // Cuando el jugador esté sobre la plataforma, desactiva el trigger para que no pueda caer
            if (other.transform.position.y > transform.position.y)
            {
                platformCollider.isTrigger = false;  
               UnityEngine.Debug.Log ("El jugador esta encima");
            }
        }
    }
}
