using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int scoreInstance;
    private Dictionary<int, Level> levelDic = new Dictionary<int, Level>();

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

    // todo migrate this logic to another class that contains logic of score/points
    public void addScore()
    {
        Instance.scoreInstance += 1;
    }
    public int getScore()
    {
        return Instance.scoreInstance;
    }

    public void addNewLevel(int levelId, Level level)
    {

        if (levelDic.ContainsKey(levelId))
        {
            levelDic[levelId] = level;
        }
        levelDic.Add(levelId, level);
    }
}
