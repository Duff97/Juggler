using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private void Start()
    {
        GameManager.OnGameSarted += Remove;
    }

    private void OnDestroy()
    {
        GameManager.OnGameSarted -= Remove;
    }

    private void Remove()
    {
        Destroy(gameObject);
    }


}
