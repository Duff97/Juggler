using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource crowdMurmuring;
    [SerializeField] private AudioSource crowdCheering;
    [SerializeField] private AudioSource crowdApplauding;
    [SerializeField] private AudioSource crowdDisappointed;

    private void Start()
    {
        TransitionManager.OnTransitionToGameStart += HandleTransitionToGameStart;
        TransitionManager.OnTransitionToTitleStart += HandleTransitionToTitleStart;
        GameManager.OnGameSarted += HandleGameStart;
        GameManager.OnGameEnded += HandleGameEnd;
    }

    private void OnDestroy()
    {
        TransitionManager.OnTransitionToGameStart -= HandleTransitionToGameStart;
        TransitionManager.OnTransitionToTitleStart -= HandleTransitionToTitleStart;
        GameManager.OnGameSarted -= HandleGameStart;
        GameManager.OnGameEnded -= HandleGameEnd;
    }

    private void HandleTransitionToGameStart()
    {
        crowdMurmuring.Stop();
        crowdCheering.Play();
    }

    private void HandleTransitionToTitleStart()
    {
        crowdApplauding.Play();
    }

    private void HandleGameStart()
    {
        crowdApplauding.Play();
    }

    private void HandleGameEnd()
    {
        crowdDisappointed.Play();
    }
}
