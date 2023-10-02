using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private bool isThrowingRight;
    [SerializeField] private float throwVelocityY;
    [SerializeField] private float throwVelocityX;

    [SerializeField] private float velocityMaxScaler;
    [SerializeField] private float velocityScalerMaxDistance;
    [SerializeField] private float velocityScalerMinDistance;

    [SerializeField] private ColliderToggle colliderToggle;

    public static event Action OnBallThrown;
    

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;
        ThrowBall(rb);
    }

    void ThrowBall(Rigidbody rb)
    {
        float directionX = isThrowingRight ? 1 : -1;
        Vector3 baseThrowVelocity = new Vector3(throwVelocityX * directionX, throwVelocityY, 0);
        float handDistance = transform.position.x * -directionX;
        float velocityScaler = CalculateVelocityScaler(handDistance);
        rb.velocity = baseThrowVelocity * velocityScaler;
        
        colliderToggle.HandleBallThrown();
        OnBallThrown?.Invoke();
    }

    float CalculateVelocityScaler(float distance)
    {
        if (distance <= velocityScalerMinDistance)
            return 1.0f;

        if (distance >= velocityScalerMaxDistance)
            return velocityMaxScaler;

        // Linear interpolation for values between min and max distances
        return Mathf.Lerp(1.0f, velocityMaxScaler, (distance - velocityScalerMinDistance) / (velocityScalerMaxDistance - velocityScalerMinDistance));
    }

}