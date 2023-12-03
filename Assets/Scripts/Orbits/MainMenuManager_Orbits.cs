using System.Collections;
using TMPro;
using UnityEngine;

public class MainMenuManager_Orbits : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestText;
    [SerializeField] private TMP_Text _bestScoreText;

    private void Awake()
    {
        _bestScoreText.text = GameManager_Orbits.Instance.HighScore.ToString();
        

        if(!GameManager_Orbits.Instance.IsInitialized)
        {
            _scoreText.gameObject.SetActive(false);
            _newBestText.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowScore());
        }
    }

    [SerializeField] private float _animationTime;
    [SerializeField] private AnimationCurve _speedCurve;

    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        _scoreText.text = tempScore.ToString();

        int currentScore = GameManager_Orbits.Instance.CurrentScore;
        int highScore = GameManager_Orbits.Instance.HighScore;

        if(currentScore > highScore)
        {
            _newBestText.gameObject.SetActive(true);
            GameManager_Orbits.Instance.HighScore = currentScore;
        }
        else
        {
            _newBestText.gameObject.SetActive(false);
        }

        float speed = 1 / _animationTime;
        float timeElapsed = 0f;
        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;

            tempScore = (int)(_speedCurve.Evaluate(timeElapsed) * currentScore);
            _scoreText.text = tempScore.ToString();

            yield return null;
        }

        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();
    }

    [SerializeField] private AudioClip _clickSound;

    public void ClickedPlay()
    {
        SoundManager_Orbits.Instance.PlaySound(_clickSound);
        GameManager_Orbits.Instance.GoToGameplay();
    }
      public void ClickedReturn()
    {
         GameManager_Orbits.Instance.Return();
    }
   




}
