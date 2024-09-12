using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrunk : MonoBehaviour
{

    public float fallDuration = 1f; // Duración de la caída
    public float fallAngle = 30f; // Ángulo de caída en grados

    private bool isFalling = false;
    private float fallStartTime;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Asegúrate de que el golpe proviene de un objeto con etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calcula la dirección del golpe
            Vector3 directionToPlayer = transform.position - collision.transform.position;
            directionToPlayer.Normalize();

            // Aplica la fuerza de caída en la dirección opuesta al jugador
            if (!isFalling)
            {
                isFalling = true;
                fallStartTime = Time.time;

                // Calcula el ángulo de rotación basado en la dirección del golpe
                float angle = fallAngle;
                Vector3 rotationAxis = Vector3.Cross(directionToPlayer, Vector3.up).normalized;

                // Calcula la rotación final
                Quaternion targetRotation = Quaternion.AngleAxis(angle, rotationAxis) * initialRotation;
                StartCoroutine(RotateOverTime(targetRotation, fallDuration));
            }
        }
    }

    private IEnumerator RotateOverTime(Quaternion targetRotation, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation; // Asegúrate de que la rotación final sea exacta
    }
 }
