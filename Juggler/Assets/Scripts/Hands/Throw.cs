using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private bool isThrowingRight;
    [SerializeField] private float throwVelocityY;
    [SerializeField] private float throwVelocityX;

    [SerializeField] private float throwMaxDistance;
    [SerializeField] private float throwMinDistance;
    [SerializeField] private Transform throwTarget;

    [SerializeField] private ColliderToggle colliderToggle;

    public static event Action OnBallThrown;
    

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;
        ThrowBall(rb);
    }

    private void ThrowBall(Rigidbody rb)
    {
        float directionX = isThrowingRight ? 1 : -1;
        Vector3 baseThrowVelocity = new Vector3(throwVelocityX * directionX, throwVelocityY, 0);
        float handDistance = (throwTarget.position.x - transform.position.x) * directionX;
        float velocityScaler = CalculateVelocityScaler(handDistance);
        baseThrowVelocity.x *= velocityScaler;
        rb.velocity = baseThrowVelocity;
        
        colliderToggle.HandleBallThrown();
        OnBallThrown?.Invoke();
    }

    private float CalculateVelocityScaler(float distance)
    {
        distance = Mathf.Clamp(distance, throwMinDistance, throwMaxDistance);

        return distance / throwMinDistance;
    }

}