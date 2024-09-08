using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollower : MonoBehaviour
{
    
    public Transform player;          // Referencia al jugador
    public float followDistance = 10f; // Distancia en la que el enemigo comienza a seguir
    public float stopDistance = 2f;    // Distancia mínima a la que el enemigo se detiene

    private NavMeshAgent navAgent;     // Componente NavMeshAgent para el movimiento

    void Start()
    {
        // Obtener el componente NavMeshAgent del enemigo
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Verifica si el NavMeshAgent está activo y en un NavMesh
        if (navAgent != null && navAgent.isOnNavMesh)
        {
            // Calcular la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // Si el jugador está dentro del rango de seguimiento y fuera de la distancia mínima
            if (distanceToPlayer < followDistance && distanceToPlayer > stopDistance)
            {
                // Mover al enemigo hacia el jugador
                navAgent.SetDestination(player.position);
            }
            else
            {
                // Detener al enemigo si está muy cerca o fuera de rango
                navAgent.ResetPath();
            }
        }
    }

    // Método para visualizar el área de seguimiento en la escena
    void OnDrawGizmosSelected()
    {
        // Dibujar un círculo para mostrar el rango de seguimiento
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);

        // Dibujar un círculo para mostrar la distancia mínima de parada
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }

    }
