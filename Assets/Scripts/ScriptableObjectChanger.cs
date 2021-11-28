using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ScriptableObject[] objectList;
    [SerializeField] private ScriptableObject[] levelDifficulty;

    [Header("Display Scripts")]
    [SerializeField] private MapDisplay mapDisplay;
    private int currentIndex;

    private void Awake()
    {
        ChangeScriptableObject(0);
    }
    public void ChangeScriptableObject(int _change)
    {
        currentIndex += _change;
        if (currentIndex < 0) currentIndex = objectList.Length - 1;
        else if (currentIndex > objectList.Length - 1) currentIndex = 0;

        if (mapDisplay != null) mapDisplay.DisplayMap((Map)objectList[currentIndex], (LevelDifficulty)levelDifficulty[currentIndex]);
    }
}
