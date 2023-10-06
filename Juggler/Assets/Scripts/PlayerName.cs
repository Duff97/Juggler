using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;

    private const string PREFIX = "Playing as ";


    private void Start()
    {
        DisplayName();
        Login.OnLoginSuccess += DisplayName;
    }

    private void OnDestroy()
    {
        Login.OnLoginSuccess -= DisplayName;
    }

    private async void DisplayName()
    {
        try
        {
            nameText.text = PREFIX + await AuthenticationService.Instance.GetPlayerNameAsync();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
