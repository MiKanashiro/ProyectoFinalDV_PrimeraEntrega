using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private GameObject mapName;
    [SerializeField] private GameObject mapDescription;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameObject TotalZombiesUI;
    private LevelDifficulty levelDifficulty;

    public void DisplayMap(Map _map, LevelDifficulty _levelDifficulty)
    {
        mapName.GetComponent<TextMeshProUGUI>().color = _map.nameColor;
        mapDescription.GetComponent<TextMeshProUGUI>().text = _map.mapDescription;
        mapName.GetComponent<TextMeshProUGUI>().text = _map.mapName;
        mapImage.sprite = _map.mapImage;
        levelDifficulty = _levelDifficulty;

        dropdown.options.Clear();
        foreach (KeyValuePair<Difficulty, int> entry in _levelDifficulty.LevelOptions)
        {
            dropdown.options.Add(
                new Dropdown.OptionData()
                {
                    text = entry.Key.ToString()
                });
        }

        UpdateZombiesDetails();
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _map.mapIndex;

        lockIcon.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        if (mapUnlocked)
            mapImage.color = Color.white;
        else
            mapImage.color = Color.gray;

        playButton.onClick.RemoveAllListeners();
        var nameScene = _map.sceneToLoad.name;
        GameManager.Instance.levelDifficulty = levelDifficulty;
        playButton.onClick.AddListener(() => SceneManager.LoadScene((nameScene)));
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value + 1;
        levelDifficulty.selectedDifficulty = (Difficulty)index;
        UpdateZombiesDetails();
    }

    void UpdateZombiesDetails()
    {
        // Todo here put the logic for show the diferents type of zombies
        var index = (Difficulty)levelDifficulty.selectedDifficulty;
        TotalZombiesUI.GetComponent<TextMeshProUGUI>().text = "Zombies:" + levelDifficulty.LevelOptions[index].ToString();
    }
}