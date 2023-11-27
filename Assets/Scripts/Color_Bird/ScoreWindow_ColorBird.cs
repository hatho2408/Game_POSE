using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreWindow_ColorBird : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI ScoreText;
     [SerializeField] private TextMeshProUGUI HightScoreText;

     private void Start() {
        HightScoreText.text="HIGHTSCORE: "+Score_ColorBird.GetHighScore().ToString();
        SendHighestScoreToAndroidApp_ColorBird();
     }
    
    private void Update()
    {
        ScoreText.text = LevelManager_ColorBird.Instance().GetPipePassedCount().ToString();
    }
    public  void SendHighestScoreToAndroidApp_ColorBird()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                // Call a method in your Android app to send the highest score
                activity.Call("sendScoreToAndroidApp", int.Parse(Score_ColorBird.GetHighScore().ToString()));
            }
        }
    }
    
}
