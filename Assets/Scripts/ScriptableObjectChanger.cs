using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ScriptableObject[] objectList;
    // We could pass this information from the array objectList to a dictionary... I thought it was a bad idea so I didn't implement it, if you want you can delete it.
    // private Dictionary<int, ScriptableObject> dictionary = new Dictionary<int, ScriptableObject>();

    [Header("Display Scripts")]
    [SerializeField] private MapDisplay mapDisplay;
    private int currentIndex;

    private void Awake()
    {
        ChangeScriptableObject(0);
    }
    public void ChangeScriptableObject(int _change)
    {
        print(_change);
        currentIndex += _change;
        if (currentIndex < 0) currentIndex = objectList.Length - 1;
        else if (currentIndex > objectList.Length - 1) currentIndex = 0;

        if (mapDisplay != null) mapDisplay.DisplayMap((Map)objectList[currentIndex]);
    }
}
