using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private Text textPlayerLives;
    [SerializeField] private Text textBullets;

    [SerializeField] private Text textAnnounce;

    [SerializeField] private PlayerController PlController;


    // Update is called once per frame
    void Start()
    {
        textPlayerLives.text = PlController.getPlayerLives() + " X";
        textScore.text = "0" + " X";
    }
    void Update()
    {
        //UpdateScoreUI();
        //UpdatePlayerLivesUI();
        //UpdateBulletsUI();
    }
    public void OnDeathHandler()
    {
       textAnnounce.text = "GAME OVER";
    }
    public void OnLivesChangeHandler(int lives)
    {
        Debug.Log("HUD controller - On Lives");
        //textPlayerLives.text = PlController.getPlayerLives() + " X";
        textPlayerLives.text = lives + " X";
    }
    public void OnHitHandler()
    {
        Debug.Log("HUD controller - On hit");
        textScore.text = GameManager.Instance.getScore() + " X";
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