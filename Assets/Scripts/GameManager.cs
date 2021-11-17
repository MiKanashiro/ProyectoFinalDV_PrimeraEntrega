using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int scoreInstanciado;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            scoreInstanciado = 0;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void addScore()
    {
        instance.scoreInstanciado += 1;
    }
    public static int getScore()
    {
        return instance.scoreInstanciado;
    }
}
