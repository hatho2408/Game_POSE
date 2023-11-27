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
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager_Orbits.Instance.GoToMainMenu();
    }
    
}
