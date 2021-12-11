using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnerController : MonoBehaviour
{


    [SerializeField]
    private UnityEvent<int> zombiesChange;

    [SerializeField]
    private GameObject worldLimits;

    public GameObject[] enemyPrefabs;
    public GameObject playerPrefab;


    private LevelDifficulty levelDifficulty;
    
    private Difficulty difficulty;

    private float startDelay = 1.0f;
    private float repeatRate = 1.0f;
    private int totalZombies = 0;

    void Start()
    {
        levelDifficulty = GameManager.Instance?.levelDifficulty ?? new LevelDifficulty();
        difficulty = levelDifficulty.selectedDifficulty;
        totalZombies = levelDifficulty.LevelOptions[difficulty];
        playerPrefab.transform.position = levelDifficulty.InitPlayerPosition;
        worldLimits.transform.position = levelDifficulty.InitWorldLimitPosition;
        switch (difficulty)
        {
            case Difficulty.Easy:
                InvokeRepeating("SpawnEnemy", startDelay + 2f, repeatRate + 2f);
                break;

            case Difficulty.Normal:
                InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
                break;

            case Difficulty.Hard:
                InvokeRepeating("SpawnEnemy", startDelay, repeatRate - 1f);
                break;
        }
    }

    public void SpawnEnemy()
    {
        float angle = Random.Range(0f, 360f);
        float distanceFromPlayer = Random.Range(20f, 25f);

        Vector3 spawnPosition = transform.position + new Vector3 (Mathf.Cos(angle),0,Mathf.Sin(angle))*distanceFromPlayer;
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
        totalZombies = totalZombies - 1;
        zombiesChange?.Invoke(levelDifficulty.LevelOptions[difficulty] - GameManager.Instance.getScore());
        if (totalZombies == 0)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
    public void OnDeathHandler()
    {
        Debug.Log("Enemy Spawn Controller - On death");
        CancelInvoke("SpawnEnemy");
    }
}

