using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This class will be used to show the level starts and the score for the user on the main menu
public class Level : MonoBehaviour
{
    public int levelId;

    [SerializeField] private int score;
    [SerializeField] private bool firstRewardEarned;
    [SerializeField] private bool secoundRewardEarned;
    [SerializeField] private bool thirdRewardEarned;
    public bool FirstRewardEarned   { get; set;}
    public bool SecoundRewardEarned { get; set; }
    public bool ThirdRewardEarned { get; set; }
    public bool Score { get; set; }

    private void Start()
    {
        AddNewLevel();
    }

    private void AddNewLevel()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.addNewLevel(levelId, this);
    }
}
