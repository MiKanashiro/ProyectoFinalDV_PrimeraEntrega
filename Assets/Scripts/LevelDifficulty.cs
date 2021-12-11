using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelDificulty", menuName = "Scriptable Object/LevelDificulty")]
public class LevelDifficulty : ScriptableObject
{

    [SerializeField]
    private int ammountDificulty = 10;
    [SerializeField]
    private int intialAmmountOfEnemy = 40;
    [SerializeField]
    private Vector3 playerPosition;
    [SerializeField]
    private Vector3 worldLimitPosition;
    
    private Dictionary<Difficulty, int> levelOptions;
    public Dictionary<Difficulty, int> LevelOptions { get { return levelOptions; } private set { } }
    public Vector3 InitPlayerPosition { get { return playerPosition; } private set { } }
    public Vector3 InitWorldLimitPosition { get { return worldLimitPosition; } private set { } }

    public Difficulty selectedDifficulty;
    


    public void OnEnable()
    {
        levelOptions = new Dictionary<Difficulty, int>();
        foreach (Difficulty diff in (Difficulty[])Enum.GetValues(typeof(Difficulty)))
        {
            int level = (int)diff;
            levelOptions.Add(diff, (level * ammountDificulty) + intialAmmountOfEnemy);
        }
        
    }
    // this function is only for dev environment --> todo enable only in dev environment
    public LevelDifficulty()
    {
        playerPosition = new Vector3(647.5f, 16.32f, 414.2f);
        worldLimitPosition = new Vector3(645, 85.32f, 386.35f);
        this.selectedDifficulty = Difficulty.Easy;
    }
}


