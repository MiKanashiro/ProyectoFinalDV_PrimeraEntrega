using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int scoreInstance;
    public LevelDifficulty levelDifficulty { get; set; }
    
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            scoreInstance = 0;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        scoreInstance = 0;
        Debug.Log("GAME OVER");
    }
    // todo migrate this logic to another class that contains logic of score/points
    public void addScore()
    {
        Instance.scoreInstance += 1;
    }
    public int getScore()
    {
        return Instance.scoreInstance;
    }

    public void OnHitHandler()
    {
        Debug.Log("Game Manasger - On hit");
        Instance.scoreInstance += 1;
    }

}
