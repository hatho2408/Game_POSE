using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager_SShip : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    Scorekeeper_SShip scorekeeper;
    public string highestScore;
     AudioPlayer_SShip audioPlayer;

    void Awake()
    {
        scorekeeper = FindObjectOfType<Scorekeeper_SShip>();
        audioPlayer = FindObjectOfType<AudioPlayer_SShip>();
    }
    public void loadGame()
    {
        scorekeeper.resetScore();
        SceneManager.LoadScene("Space_Ship_GameScene");
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("Space_Ship_MainMenu");
    }
    public void loadGameOver()
    {
        StartCoroutine(WaitAndLoad("Space_Ship_GameOver", sceneLoadDelay));
    }
      public void loadMainGameMenu()
    {
        audioPlayer.muteMusic();
        StartCoroutine(WaitAndLoad("MainMenuGame", sceneLoadDelay));
    }

    public void quitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    public void saveScore()
    {
        string timePlay = DateTime.Now.ToString();
        highestScore = scorekeeper.getScore().ToString();
        string scoreMessage = timePlay + ":" + "Your highest score is: " + highestScore;
        PlayerPrefs.SetString("Your highest score data: ", scoreMessage);
        PlayerPrefs.Save();
        Debug.Log("Saved data: " + scoreMessage);
        SendHighestScoreToAndroidApp_Ship();
    }

    void SendHighestScoreToAndroidApp_Ship()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                // Call a method in your Android app to send the highest score
                activity.Call("SendHighestScoreToAndroidApp_Ship", int.Parse(highestScore));
            }
        }
    }
}
