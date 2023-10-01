using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text gameScoreText;
    [SerializeField] private TMP_Text endGameScoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        Throw.OnBallThrown += HandleBallThrown;
        GameManager.OnGameSarted += HandleGameStarted;
        GameManager.OnGameEnded += HandleGameEnded;
    }

    private void OnDestroy()
    {
        Throw.OnBallThrown -= HandleBallThrown;
        GameManager.OnGameSarted -= HandleGameStarted;
        GameManager.OnGameEnded -= HandleGameEnded;
    }

    private void HandleBallThrown()
    {
        score++;
        gameScoreText.text = "Ball Thrown : " + score;
    }

    private void HandleGameEnded()
    {
        endGameScoreText.text = "You threw " + score + " balls!";
    }

    private void HandleGameStarted()
    {
        score = -1;
        HandleBallThrown();
    }
}
