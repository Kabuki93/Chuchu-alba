using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafCollisionDetection : MonoBehaviour
{
     private Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();  // Obtiene el Collider de la plataforma
    }

    private void OnTriggerStay(Collider other)
    {
        // Ahora verificamos si el objeto tiene el tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            CapsuleCollider capsule = other.GetComponent<CapsuleCollider>();
            
            // Verifica que el CapsuleCollider fue encontrado
            if (capsule != null)
            {
                // Debug para ver las posiciones en el log
                Debug.Log("Comparando posiciones...");
                Debug.Log("Posición Y del objeto 'Player': " + other.transform.position.y);
                Debug.Log("Posición Y de la plataforma: " + transform.position.y);
                Debug.Log("Altura del CapsuleCollider: " + capsule.height);

                // Comparación para desactivar el Collider de la plataforma si el jugador está por debajo
                if (other.transform.position.y - capsule.height / 4 < transform.position.y)
                {
                    if (coll.enabled)
                    {
                        Debug.Log("Desactivando el Collider de la plataforma.");
                        coll.enabled = false;   // Desactiva el Collider de la plataforma
                    }
                }
                else
                {
                    if (!coll.enabled)
                    {
                        Debug.Log("Activando el Collider de la plataforma.");
                        coll.enabled = true;  // Activa el Collider de la plataforma
                    }
                }
            }
            else
            {
                Debug.LogError("El objeto 'Player' no tiene un CapsuleCollider.");
            }
        }
    }
}

