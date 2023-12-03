using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Pixel : MonoBehaviour
{
    public static GameManager_Pixel instance;

    public int difficulty;

    [Header("Timer info")]
    public float timer;
    public bool startTime;

    [Header("Level info")]
    public int levelNumber;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        if (difficulty == 0)
            difficulty = PlayerPrefs.GetInt("GameDifficulty");
    }

    private void Update()
    {
        if (startTime)
            timer += Time.deltaTime;

    }


    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }
    public void SaveBestTime()
    {
        startTime = false;


        float lastTime = PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime",999);

        if(timer < lastTime)
            PlayerPrefs.SetFloat("Level" + levelNumber + "BestTime", timer);

        timer = 0;
    }
    public void SaveCollectedFruits()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");

        int newTotalFruits = totalFruits + PlayerManager_Pixel.instance.fruits;


        PlayerPrefs.SetInt("TotalFruitsCollected", newTotalFruits);
        SendHighestScoreToAndroidApp_Pixel();
        PlayerPrefs.SetInt("Level" + levelNumber + "FruitsCollected", PlayerManager_Pixel.instance.fruits);
        PlayerManager_Pixel.instance.fruits = 0;
    }
    public void SaveLevelInfo()
    {
        int nextLevelNumber = levelNumber + 1;
        PlayerPrefs.SetInt("Level" + nextLevelNumber + "Unlocked",1);
    }
     public void SendHighestScoreToAndroidApp_Pixel()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                // Call a method in your Android app to send the highest score
                int highScore = GameManager_DotRescue.Instance.HighScore;
                activity.Call("SendHighestScoreToAndroidApp_Pixel", highScore);
            }
        }
    }
    
}
