using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score_ColorBird
{
    public static void Start()
    {
        
        BirdController_ColorBird.Instance().onDied+=Score_onDied;
    }

    private static void Score_onDied(object sender, EventArgs e)
    {
        SetNewHightScore(LevelManager_ColorBird.Instance().GetPipePassedCount());
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HightScore");
    }
    public static void SetHightScore(int score)
    {
        PlayerPrefs.SetInt("HightScore", score);
        PlayerPrefs.Save();
    }
    public static bool SetNewHightScore(int score)
    {
        int currentScore = GetHighScore();
        if (score > currentScore)
        {
            SetHightScore(score);
            return true;
        }
        else
        {
            return false;
        }
    }
    //  void SendHighestScoreToAndroidApp()
    // {
    //     using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    //     {
    //         using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
    //         {
    //             // Call a method in your Android app to send the highest score
    //             activity.Call("sendScoreToAndroidApp", int.Parse(highestScore));
    //         }
    //     }
    // }
    
}
