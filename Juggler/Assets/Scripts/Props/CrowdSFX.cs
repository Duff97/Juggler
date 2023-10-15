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
        TransitionManager.OnTransitionToTitleStart += HandleTransitionToTitleStart;
        GameManager.OnGameSarted += PlayApplauds;
        GameManager.OnGameEnded += HandleGameEnd;
        Ball.OnFirstThrow += PlayApplauds;
    }

    private void OnDestroy()
    {
        TransitionManager.OnTransitionToGameStart -= HandleTransitionToGameStart;
        TransitionManager.OnTransitionToTitleStart -= HandleTransitionToTitleStart;
        GameManager.OnGameSarted -= PlayApplauds;
        GameManager.OnGameEnded -= HandleGameEnd;
        Ball.OnFirstThrow -= PlayApplauds;
    }

    private void HandleTransitionToGameStart()
    {
        CancelInvoke(nameof(PlayMurmurs));
        crowdMurmuring.Stop();
        crowdCheering.Play();
    }
    private void HandleTransitionToTitleStart() 
    {
        PlayApplauds();
        Invoke(nameof(PlayMurmurs), 3);
    }

    private void HandleGameEnd()
    {
        crowdDisappointed.Play();
    }

    private void PlayApplauds()
    {
        crowdApplauding.Play();
    }

    private void PlayMurmurs()
    {
        crowdMurmuring.Play();
    }
}
