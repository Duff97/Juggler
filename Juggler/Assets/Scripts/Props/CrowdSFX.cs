using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSFX : MonoBehaviour
{
    [SerializeField] private AudioSource crowdMurmuring;
    [SerializeField] private AudioSource crowdCheering;
    [SerializeField] private AudioSource crowdApplauding;
    [SerializeField] private AudioSource crowdDisappointed;

    private void Start()
    {
        TransitionManager.OnTransitionToGameStart += HandleTransitionToGameStart;
        TransitionManager.OnTransitionToTitleStart += PlayApplauds;
        GameManager.OnGameSarted += PlayApplauds;
        GameManager.OnGameEnded += HandleGameEnd;
        Ball.OnFirstThrow += PlayApplauds;
    }

    private void OnDestroy()
    {
        TransitionManager.OnTransitionToGameStart -= HandleTransitionToGameStart;
        TransitionManager.OnTransitionToTitleStart -= PlayApplauds;
        GameManager.OnGameSarted -= PlayApplauds;
        GameManager.OnGameEnded -= HandleGameEnd;
        Ball.OnFirstThrow -= PlayApplauds;
    }

    private void HandleTransitionToGameStart()
    {
        crowdMurmuring.Stop();
        crowdCheering.Play();
    }

    private void HandleGameEnd()
    {
        crowdDisappointed.Play();
    }

    private void PlayApplauds()
    {
        crowdApplauding.Play();
    }
}
