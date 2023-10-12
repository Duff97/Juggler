using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Login : MonoBehaviour
{

    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private TMP_InputField nameInput;

    public static event Action OnLoginSuccess;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        if (!await SignInCachedUser()) return;

        completeLogin();
    }

    public async void LoginNewUser()
    {
        string name = nameInput.text;

        if (name.Length == 0 || name.Length > 30) return;

        var filter = new ProfanityFilter.ProfanityFilter();

        if (filter.IsProfanity(name))
        {
            Debug.Log("PROFANITY DETECTED");
            return;
        }

        await SignInNewUser(name);

        completeLogin();

    }

    private void completeLogin()
    {
        loginPanel.SetActive(false);
        titlePanel.SetActive(true);
        OnLoginSuccess?.Invoke();
    }

    private async Task<bool> SignInCachedUser()
    {
        if (!AuthenticationService.Instance.SessionTokenExists) return false;

        return await SignIn();
    }

    private async Task<bool> SignInNewUser(string name)
    {
        await CleanupSession();

        if (!await SignIn()) return false;

        await AuthenticationService.Instance.UpdatePlayerNameAsync(name);

        return true;
    }

    private async Task<bool> SignIn()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return false;
    }

    private async void OnApplicationQuit()
    {
        await CleanupSession();
    }

    private async Task CleanupSession()
    {
        if (!AuthenticationService.Instance.IsSignedIn) return;

        await AuthenticationService.Instance.DeleteAccountAsync();
        AuthenticationService.Instance.SignOut();
        AuthenticationService.Instance.ClearSessionToken();
    }
}
