using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
public string playerTag = "Player"; // Tag del jugador
    private bool isActivated = false; // Para asegurarse de que solo se active una vez
    public cotroladoragua waterController; // Referencia al controlador del agua

    private void OnTriggerEnter(Collider other)
    {
        // Verificar que el collider activado tiene el tag del jugador y que el botón no esté ya activado
        if (other.CompareTag(playerTag) && !isActivated)
        {
            Debug.Log("Player pressed button: " + this.name);
            isActivated = true; // Marcar el botón como activado

            // Llamar al controlador del agua para incrementar el contador
            waterController.IncrementColliderCount();

            // Desactivar solo el collider del botón, pero solo si no está ya desactivado
            Collider collider = GetComponent<Collider>();
            if (collider != null && collider.enabled)
            {
                collider.enabled = false; // Desactivar el collider para que no se active de nuevo
            }
        }
    }
}

