using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Login : MonoBehaviour
{
    [Header("Name rules")]
    [SerializeField] private int MinNameLength;
    [SerializeField] private int MaxNameLength;

    [Header("References")]
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private TMP_InputField nameInput;

    public static event Action OnLoginSuccess;
    public static event Action OnLoginFailure;
    public static event Action OnPlayerNameChanged;

    private static readonly Regex AlphanumericRegex = new Regex("^[a-zA-Z0-9]*$");

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        
        if (await SignIn())
            loginPanel.SetActive(true);
        else
            titlePanel.SetActive(true);
    }

    public async void ChangePlayerNameAsync()
    {
        string name = nameInput.text;

        if (!IsValidName(name))
        {
            Message.Instance.DisplayMessage("The name you entered is not valid or contains bad words :(<br><br>You were assigned a random name instead");
            return;
        }

        await AuthenticationService.Instance.UpdatePlayerNameAsync(name);

        OnPlayerNameChanged?.Invoke();
    }

    private bool IsValidName(string name)
    {
        var filter = new ProfanityFilter.ProfanityFilter();

        return name.Length >= MinNameLength &&
               name.Length <= MaxNameLength &&
               AlphanumericRegex.IsMatch(name) &&
               !filter.IsProfanity(name);
    }

    private async Task<bool> SignIn()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            OnLoginSuccess?.Invoke();

            return true;
        }
        catch (Exception ex)
        {
            Message.Instance.DisplayMessage("<b>Connection error</b><br><br>You will not have access to the leaderboard");
            OnLoginFailure?.Invoke();
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
