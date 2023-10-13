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
    [SerializeField] private GameObject ScoreContainer;
    [SerializeField] private ScoreItem ScoreItemPrefab;
    [SerializeField] private GameObject ErrorText;

    private void Start()
    {
        Login.OnLoginSuccess += RefreshScores;
    }

    private void OnDestroy()
    {
        Login.OnLoginSuccess -= RefreshScores;
    }

    public async void RefreshScores()
    {
        foreach (Transform child in ScoreContainer.transform) { Destroy(child.gameObject); }
        
        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(Configuration.Instance.GetLeaderboardId());

            ErrorText.SetActive(false);

            bool alt = false;

            for (int i = 0; i < scores.Results.Count && i < maxEntries; i++)
            {
                var score = scores.Results[i];
                ScoreItem scoreItem = Instantiate(ScoreItemPrefab);
                scoreItem.SetValues(score.Rank + 1, score.PlayerName, score.Score, alt);
                scoreItem.transform.SetParent(ScoreContainer.transform, false);
                alt = !alt;
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
            ErrorText.SetActive(true);

        }
    }

    public async void AddScore(int score)
    {
        var scores = await LeaderboardsService.Instance.GetScoresAsync(Configuration.Instance.GetLeaderboardId());
        if (scores.Results.Count >= maxEntries && score <= scores.Results[maxEntries - 1].Score) return;

        await LeaderboardsService.Instance.AddPlayerScoreAsync(Configuration.Instance.GetLeaderboardId(), score);
        RefreshScores();
    }
        
}
