using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int scoreInstance;

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

    // Update is called once per frame
    void Update()
    {
        
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
}
