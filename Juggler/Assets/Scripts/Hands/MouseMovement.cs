using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
 
    [SerializeField] private float maxSpeed;
    [SerializeField] private float sensitivityScale;
    [SerializeField, Range(0, 1)] private int mouseButton;

    private bool movementActive = false;
    private Vector3 initialPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        GameManager.OnGameSarted += ActivateMovement;
        TransitionManager.OnTransitionToGameStart += ActivateMovement;
        GameManager.OnGameEnded += DeactivateMovement;
    }

    private void OnDestroy()
    {
        GameManager.OnGameSarted -= ActivateMovement;
        GameManager.OnGameEnded -= DeactivateMovement;
        TransitionManager.OnTransitionToGameStart -= ActivateMovement;
    }

    private void FixedUpdate()
    {

        if (!movementActive || Input.GetMouseButton(mouseButton)) {
            rb.velocity = Vector3.zero;
            return; 
        }

        // Get the mouse input in screen coordinates
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * sensitivityScale;

        if (mouseMovement.magnitude > maxSpeed)
            mouseMovement = mouseMovement.normalized * maxSpeed;

        // Move the object based on mouse movement
        rb.velocity = mouseMovement;
    }

    private void ActivateMovement()
    {
        if (movementActive) return;

        transform.position = initialPosition;
        movementActive = true;
    }

    private void DeactivateMovement()
    {
        movementActive = false;
    }
}
