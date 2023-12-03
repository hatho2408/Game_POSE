using System.Collections;
using TMPro;
using UnityEngine;

public class GameplayManager_Orbits : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _scorePrefab;

    private int score;

    private void Awake()
    {
        GameManager_Orbits.Instance.IsInitialized = true;

        score = 0;
        _scoreText.text = score.ToString();
        SpawnScore();
    }

    public void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
        SpawnScore();
    }

    private void SpawnScore()
    {
        Instantiate(_scorePrefab);
    }

    public void GameEnded()
    {
        GameManager_Orbits.Instance.CurrentScore = score;
        SendHighestScoreToAndroidApp_Orbits();
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager_Orbits.Instance.GoToMainMenu();
    }
       public  void SendHighestScoreToAndroidApp_Orbits()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                // Call a method in your Android app to send the highest score
                 int highScore = GameManager_Orbits.Instance.HighScore;
                activity.Call("SendHighestScoreToAndroidApp_Orbits", highScore);
            }
        }
    }
    
}
