using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    
    enum Difficulties {Easy=1, Normal, Hard};
    [SerializeField] private Difficulties difficulty;

    private float startDelay = 1.0f;
    private float repeatRate = 1.0f;

    void Start()
    {
        switch (difficulty)
        {
            case Difficulties.Easy:
                InvokeRepeating("SpawnEnemy", startDelay + 2f, repeatRate + 2f);
                break;

            case Difficulties.Normal:
                InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
                break;

            case Difficulties.Hard:
                InvokeRepeating("SpawnEnemy", startDelay, repeatRate - 1f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        float angle = Random.Range(0f, 360f);
        float distanceFromPlayer = Random.Range(20f, 25f);

        Vector3 spawnPosition = playerPrefab.transform.position + new Vector3 (Mathf.Cos(angle),0,Mathf.Sin(angle))*distanceFromPlayer;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

