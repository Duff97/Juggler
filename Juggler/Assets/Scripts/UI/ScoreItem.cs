using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private Image background;

    private const float ALT_ALPHA = 0.5f;

    public void SetValues(int rank, string name, double score, bool altColor)
    {
        rankText.text = rank.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();

        if (!altColor) return;

        Color tmpColor = background.color;
        tmpColor.a = ALT_ALPHA;

        background.color = tmpColor;
    }
}
