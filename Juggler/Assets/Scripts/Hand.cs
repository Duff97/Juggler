using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        GameManager.OnGameSarted += HandleGameStarted;
    }

    private void OnDestroy()
    {
        GameManager.OnGameSarted -= HandleGameStarted;
    }

    private void HandleGameStarted()
    {
        transform.position = initialPosition;
    }
}
