using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        Throw.OnBallThrown += HandleBallThrown;
    }

    private void OnDestroy()
    {
        Throw.OnBallThrown -= HandleBallThrown;
    }

    private void HandleBallThrown()
    {
        score++;
        scoreText.text = "Ball Thrown : " + score;
    }
}
