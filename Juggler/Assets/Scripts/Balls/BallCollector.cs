using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollector : MonoBehaviour
{
    public static event Action OnBallCollected;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        OnBallCollected?.Invoke();
    }
}
