using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private Text textPlayerLives;
    [SerializeField] private Text textBullets;
    [SerializeField] private TMP_Text totalZombies;

    [SerializeField] private Text textAnnounce;
    [SerializeField] private Button continueButton;

    [SerializeField] private PlayerController PlController;


    // Update is called once per frame
    void Start()
    {
        textPlayerLives.text = PlController.getPlayerLives() + " X";
        textScore.text = "0" + " X";
       
    }
    void Update()
    {
        if (GameManager.Instance.LevelFinish())
            DisplayWinUI();
        else
            UpdateScoreUI();
        //UpdatePlayerLivesUI();
        //UpdateBulletsUI();
    }
    public void OnDeathHandler()
    {
        textAnnounce.text = "GAME OVER";
        continueButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
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

    public void DisplayWinUI()
    {
        textAnnounce.color = Color.green;
        textAnnounce.text = "Level Finish";
        Time.timeScale = 0f;
        continueButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void NavigateToSelectMenuLevel()
    {
        if(GameManager.Instance.LevelFinish())
            PlayerPrefs.SetInt(GameManager.LEVEL_KEY, PlayerPrefs.GetInt(GameManager.LEVEL_KEY) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnZombiesReminderHandler(int zombies)
    {
        Debug.Log("HUD controller - On Zombies");
        totalZombies.text = "Zombies " + zombies;
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