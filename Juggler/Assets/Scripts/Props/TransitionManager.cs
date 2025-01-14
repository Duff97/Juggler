using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Light backstageLight;
    [SerializeField] private Light[] spotlights;
    [SerializeField] private Curtain curtain;

    public static event Action OnTransitionToGame;
    public static event Action OnTransitionToGameStart;

    public static event Action OnTransitionToTitle;
    public static event Action OnTransitionToTitleStart;

    private void Start()
    {
        Curtain.OnOpened += HandleCurtainOpened;
        Curtain.OnClosed += HandleCurtainClosed;
    }

    private void OnDestroy()
    {
        Curtain.OnOpened -= HandleCurtainOpened;
        Curtain.OnClosed -= HandleCurtainClosed;
    }

    public void GameTransition()
    {
        OnTransitionToGameStart.Invoke();
        ToggleLights(false);
        curtain.Open();
        
    }

    public void TitleTransition()
    {
        OnTransitionToTitleStart?.Invoke();
        ToggleLights(true);
        curtain.Close();
    }


    private void ToggleLights(bool backstage)
    {
        backstageLight.enabled = backstage;
        foreach (Light light in spotlights)
        {
            light.enabled = !backstage;
        }
    }

    private void HandleCurtainOpened()
    {
        OnTransitionToGame?.Invoke();
    }

    private void HandleCurtainClosed()
    {
        OnTransitionToTitle?.Invoke();
    }
}
