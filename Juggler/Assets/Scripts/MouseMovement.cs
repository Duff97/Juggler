using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
 
    public float speed;

    private void Update()
    {
        // Get the mouse input in screen coordinates
        Vector3 moveDirection = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0).normalized;

        // Move the object based on mouse movement
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
