using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    
    enum Difficulties {Easy=1, Normal, Hard};
    [SerializeField] private Difficulties difficulty;
    public Vector3 newPosition;
    private float startDelay = 1.0f;
    private float repeatRate = 2.0f;

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

        newPosition = playerPrefab.transform.position + new Vector3 (Mathf.Cos(angle),0,Mathf.Sin(angle))*20;
        Instantiate(enemyPrefab, newPosition, Quaternion.identity);
    }
}

