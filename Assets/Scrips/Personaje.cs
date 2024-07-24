using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour

{ 
     public float moveSpeed = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public GameObject train; // Referencia al tren
     public Transform trainEntrance; 

    private Rigidbody rb;
    private bool isGrounded;
    private bool isOnTrain;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
        
        Move();

        if (isOnTrain && Input.GetKeyDown(KeyCode.E))
        {
            EnterTrain();
        }

          if (isOnTrain && Input.GetKeyDown(KeyCode.Q))
        {
            ExitTrain();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == train)
        {
            isOnTrain = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == train)
        {
            isOnTrain = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    void EnterTrain()
    {
        
        transform.position = trainEntrance.position;

        
        canMove = false;
    }

    void ExitTrain()
    {

        transform.position = trainEntrance.position + Vector3.up; 

        canMove = true;
    }
}

