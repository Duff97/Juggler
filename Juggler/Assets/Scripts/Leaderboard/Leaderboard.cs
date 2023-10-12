using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private int maxEntries;

    [SerializeField] private GameObject ScoreContainer;
    [SerializeField] private ScoreItem ScoreItemPrefab;

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

        var scores = await LeaderboardsService.Instance.GetScoresAsync(Configuration.Instance.GetLeaderboardId());

        bool alt = false;

        for (int i = 0; i < scores.Results.Count && i < maxEntries; i++) {
            var score = scores.Results[i];
            ScoreItem scoreItem = Instantiate(ScoreItemPrefab);
            scoreItem.SetValues(score.Rank + 1, score.PlayerName, score.Score, alt);
            scoreItem.transform.SetParent(ScoreContainer.transform, false);
            alt = !alt;
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
