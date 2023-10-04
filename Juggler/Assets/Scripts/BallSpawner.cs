using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject ballPreviewObj;
    [SerializeField] private Collider zone;

    [SerializeField] private float spawnInterval;
    [SerializeField] private float initialSpawnTime;

    private Vector3 nextSpawnPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameSarted += HandleGameStart;
        GameManager.OnGameEnded += HandleGameEnd;
    }

    private void OnDestroy()
    {
        GameManager.OnGameSarted -= HandleGameStart;
        GameManager.OnGameEnded -= HandleGameEnd;
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.position = nextSpawnPosition;
        PlaceBallPreview();
        Invoke(nameof(SpawnBall), spawnInterval);
    }

    private float GetRandomXPos()
    {
        Bounds bounds = zone.bounds;
        return Random.Range(bounds.min.x, bounds.max.x);
    }

    private void HandleGameStart()
    {
        PlaceBallPreview();
        Invoke(nameof(SpawnBall), initialSpawnTime);
    }

    private void HandleGameEnd() 
    {
        CancelInvoke(nameof(SpawnBall));
    }

    private void PlaceBallPreview()
    {
        nextSpawnPosition = new Vector3(GetRandomXPos(), transform.position.y, transform.position.z);
        ballPreviewObj.transform.position = nextSpawnPosition;
    }
}
