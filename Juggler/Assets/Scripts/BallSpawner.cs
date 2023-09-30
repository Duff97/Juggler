using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Collider zone;

    [SerializeField] private float spawnInterval;
    [SerializeField] private float initialSpawnTime;
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SpawnBall), initialSpawnTime);
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.position = new Vector3(GetRandomXPos(), transform.position.y, transform.position.z);
        Invoke(nameof(SpawnBall), spawnInterval);
    }

    private float GetRandomXPos()
    {
        Bounds bounds = zone.bounds;
        return Random.Range(bounds.min.x, bounds.max.x);
    }
}
