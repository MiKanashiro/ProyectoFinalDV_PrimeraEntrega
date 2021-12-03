using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private Text textPlayerLives;
    [SerializeField] private Text textBullets;

    [SerializeField] private PlayerController PlController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreUI();
        UpdatePlayerLivesUI();
        UpdateBulletsUI();
    }

    void UpdateScoreUI()
    {
        textScore.text = GameManager.Instance.getScore() + " X";
    }

    void UpdatePlayerLivesUI()
    {
        textPlayerLives.text = PlController.getPlayerLives() + " X" ;
    }

    void UpdateBulletsUI()
    {
        //textBullets.text =  + " X";
    }
}
