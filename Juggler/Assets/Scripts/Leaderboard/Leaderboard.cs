using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System;

public class Leaderboard : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int maxEntries;

    [Header("References")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private ScoreItem scoreItemPrefab;
    [SerializeField] private GameObject errorText;

    private void Start()
    {
        Login.OnLoginSuccess += RefreshScores;
        GameManager.OnGameEnded += ShowLeaderBoard;
        GameManager.OnGameSarted += HideLeaderBoard;
        TransitionManager.OnTransitionToGameStart += HideLeaderBoard;

    }

    private void OnDestroy()
    {
        Login.OnLoginSuccess -= RefreshScores;
        GameManager.OnGameEnded -= ShowLeaderBoard;
        GameManager.OnGameSarted -= HideLeaderBoard;
        TransitionManager.OnTransitionToGameStart -= HideLeaderBoard;
    }

    public async void RefreshScores()
    {
        foreach (Transform child in scoreContainer.transform) { Destroy(child.gameObject); }
        
        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(Configuration.Instance.GetLeaderboardId());

            errorText.SetActive(false);

            bool alt = false;

            for (int i = 0; i < scores.Results.Count && i < maxEntries; i++)
            {
                var score = scores.Results[i];
                ScoreItem scoreItem = Instantiate(scoreItemPrefab);
                scoreItem.SetValues(score.Rank + 1, score.PlayerName, score.Score, alt);
                scoreItem.transform.SetParent(scoreContainer.transform, false);
                alt = !alt;
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
            errorText.SetActive(true);

        }
    }

    public async void AddScore(int score)
    {
        var scores = await LeaderboardsService.Instance.GetScoresAsync(Configuration.Instance.GetLeaderboardId());
        if (scores.Results.Count >= maxEntries && score <= scores.Results[maxEntries - 1].Score) return;

        await LeaderboardsService.Instance.AddPlayerScoreAsync(Configuration.Instance.GetLeaderboardId(), score);
        RefreshScores();
    }

    private void HideLeaderBoard()
    {
        scorePanel.SetActive(false);
    }

    private void ShowLeaderBoard()
    {
        scorePanel.SetActive(true);
    }

}
