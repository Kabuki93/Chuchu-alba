using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("PLAYER TWO/Platformer Project/Misc/Panel")]
public class Panel : MonoBehaviour
{
     public bool autoToggle;
    public bool requireStomp;
    public bool requirePlayer;
    public AudioClip activateClip;
    public AudioClip deactivateClip;
    public GameObject waterObject; // Referencia al objeto de agua
    public int requiredButtons = 3; // Número de botones necesarios para activar el agua

    /// <summary>
    /// Called when the Panel is activated.
    /// </summary>
    public UnityEvent OnActivate;

    /// <summary>
    /// Called when the Panel is deactivated.
    /// </summary>
    public UnityEvent OnDeactivate;

    private Collider m_collider;
    private AudioSource m_audio;

    private HashSet<Collider> activatedButtons = new HashSet<Collider>(); // Conjunto para almacenar botones activados

    /// <summary>
    /// Return true if the Panel is activated.
    /// </summary>
    public bool activated { get; protected set; }

    /// <summary>
    /// Activate this Panel.
    /// </summary>
    public virtual void Activate()
    {
        if (!activated)
        {
            if (activateClip)
            {
                m_audio.PlayOneShot(activateClip);
            }

            activated = true;
            OnActivate?.Invoke();

            // Activar el agua solo si el número de botones activados es suficiente
            if (waterObject != null)
            {
                waterObject.SetActive(activatedButtons.Count >= requiredButtons);
            }
        }
    }

    /// <summary>
    /// Deactivates this Panel.
    /// </summary>
    public virtual void Deactivate()
    {
        if (activated)
        {
            if (deactivateClip)
            {
                m_audio.PlayOneShot(deactivateClip);
            }

            activated = false;
            OnDeactivate?.Invoke();

            // Desactivar el agua cuando el panel se desactiva
            if (waterObject != null)
            {
                waterObject.SetActive(false);
            }
        }
    }

    protected virtual void Start()
    {
        // Asigna un tag predeterminado si GameTags.Panel no está definido
        gameObject.tag = "Panel";
        m_collider = GetComponent<Collider>();
        m_audio = GetComponent<AudioSource>();

        // Asegúrate de que el agua esté inicialmente oculta
        if (waterObject != null)
        {
            waterObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        if (autoToggle && activatedButtons.Count < requiredButtons)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Aquí puedes añadir lógica para determinar si el "other" es un botón válido
        if (activatedButtons.Add(other))
        {
            Debug.Log("Button activated: " + other.name);
            CheckActivationStatus();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Se elimina el botón de la lista solo si es necesario (opcional)
        // Puedes comentar esta parte si no quieres que los botones se desactiven al soltar
        if (activatedButtons.Remove(other))
        {
            Debug.Log("Button deactivated: " + other.name);
            // No llamamos a CheckDeactivationStatus porque queremos que la activación sea única
        }
    }

    private void CheckActivationStatus()
    {
        if (!activated && activatedButtons.Count >= requiredButtons)
        {
            Activate();
        }
    }
}

