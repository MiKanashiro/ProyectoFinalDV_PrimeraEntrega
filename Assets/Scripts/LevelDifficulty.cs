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

    private Dictionary<Difficulty, int> levelOptions;
    public Dictionary<Difficulty, int> LevelOptions { get { return levelOptions; } private set { } }

    public Difficulty selectedDifficulty;


    public void OnEnable()
    {
        levelOptions = new Dictionary<Difficulty, int>();
        foreach (Difficulty diff in (Difficulty[])Enum.GetValues(typeof(Difficulty)))
        {
            int level = (int)diff;
            Debug.Log((level * ammountDificulty) + intialAmmountOfEnemy);
            levelOptions.Add(diff, (level * ammountDificulty) + intialAmmountOfEnemy);
        }
    }
}


