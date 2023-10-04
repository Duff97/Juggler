using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] GameStartEnable;
    [SerializeField] private GameObject[] GameEndEnable;
    [SerializeField] private GameObject[] GameStartDisable;
    [SerializeField] private GameObject[] GameEndDisable;

    public static event Action OnGameSarted;
    public static event Action OnGameEnded;

    private bool gameStarted;

    private void Start()
    {
        BallCollector.OnBallCollected += HandleBallCollected;
    }

    private void OnDestroy()
    {
        BallCollector.OnBallCollected -= HandleBallCollected;
    }

    public void StartGame()
    {
        OnGameSarted?.Invoke();
        EnableObjects(true, GameStartEnable);
        EnableObjects(false, GameStartDisable);
        gameStarted = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EndGame() 
    {
        OnGameEnded?.Invoke();
        EnableObjects(true, GameEndEnable);
        EnableObjects(false, GameEndDisable);
        gameStarted = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void EnableObjects(bool enable, GameObject[] objects) 
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(enable);
        }
    }

    private void HandleBallCollected()
    {
        if (!gameStarted) { return; }

        EndGame();
    }
}
