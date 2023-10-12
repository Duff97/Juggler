using System;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private bool hasBeenThrown = false;

    public static event Action OnFirstThrow;

    private void Start()
    {
        GameManager.OnGameSarted += Remove;
    }

    private void OnDestroy()
    {
        GameManager.OnGameSarted -= Remove;
    }

    private void Remove()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenThrown) return;

        hasBeenThrown = true;
        OnFirstThrow?.Invoke();
    }


}
