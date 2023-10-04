using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
 
    [SerializeField] private float maxSpeed;
    [SerializeField] private float sensitivityScale;
    [SerializeField, Range(0, 1)] private int mouseButton;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (Input.GetMouseButton(mouseButton)) {
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
}
