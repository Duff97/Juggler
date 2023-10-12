using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    private enum Environment
    {
        DEVELOPMENT, PRODUCTION
    }

    [SerializeField] private Environment env;

    public static Configuration Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public string GetLeaderboardId()
    {
        return env == Environment.PRODUCTION ? "Top100" : "Top100Dev";
    }
}
