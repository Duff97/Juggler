using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float throwImpulseY;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;
        rb.velocity = Vector3.zero;
        rb.AddForce(CalculateThrowForce(), ForceMode.Impulse);
    }

    Vector3 CalculateThrowForce()
    {
        // Calculate the direction from the left hand to the right hand
        Vector3 direction = (target.position - transform.position).normalized;

        // Set the x-axis force based on the normalized direction and the desired y-axis force
        float throwImpulseX = direction.x * throwImpulseY;

        // Create and return the resulting throw force vector
        return new Vector3(throwImpulseX, throwImpulseY, 0f);
    }
}
