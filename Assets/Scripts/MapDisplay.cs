using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text mapName;
    [SerializeField] private Text mapDescription;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;

    public void DisplayMap(Map _map)
    {
        print(_map.mapName);
        mapName.color = _map.nameColor;
        mapDescription.text = _map.mapDescription;
        mapName.text = _map.mapName;
        mapImage.sprite = _map.mapImage;

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _map.mapIndex;

        lockIcon.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        if (mapUnlocked)
            mapImage.color = Color.white;
        else
            mapImage.color = Color.gray;

        playButton.onClick.RemoveAllListeners();
        var nameScene = _map.sceneToLoad.name;
        playButton.onClick.AddListener(() => SceneManager.LoadScene((nameScene)));
    }
}