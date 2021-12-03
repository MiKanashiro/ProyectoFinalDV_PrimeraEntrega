using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="New Map",menuName ="Scriptable Object/Map")]
public class Map : ScriptableObject
{
    public int mapIndex;
    public string mapName;
    public string mapDescription;
    public Color nameColor;
    public Sprite mapImage;
    public Object sceneToLoad;
}
