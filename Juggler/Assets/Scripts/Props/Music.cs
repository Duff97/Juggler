using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameSarted += HandleGameStart;
        GameManager.OnGameEnded += HandleGameEnd;
    }

    
    private void HandleGameStart()
    {
        musicSource.Play();
    }

    private void HandleGameEnd()
    {
        musicSource.Stop();
    }
}
