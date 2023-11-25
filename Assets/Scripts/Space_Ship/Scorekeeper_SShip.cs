using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper_SShip : MonoBehaviour
{
    int score;
    static Scorekeeper_SShip instance;

    
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int getScore()
    {
        return score;
    }

    public void modifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score,0,int.MaxValue);
        Debug.Log(score);
    }

    public void resetScore()
    {
        score = 0;
    }
}
