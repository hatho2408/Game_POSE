using System.Collections;
using TMPro;
using UnityEngine;

public class MainMenuManager_DotRescue : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestText;
    [SerializeField] private TMP_Text _bestScoreText;

    private void Awake()
    {

        _bestScoreText.text = GameManager_DotRescue.Instance.HighScore.ToString();
        // SendHighestScoreToAndroidApp_DotRescue();

        if(!GameManager_DotRescue.Instance.IsInitialized)
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

        int currentScore = GameManager_DotRescue.Instance.CurrentScore;
        int highScore = GameManager_DotRescue.Instance.HighScore;

        if(highScore < currentScore)
        {
            _newBestText.gameObject.SetActive(true);
            GameManager_DotRescue.Instance.HighScore = currentScore;
        }
        else
        {
            _newBestText.gameObject.SetActive(false);
        }

        _bestScoreText.text = GameManager_DotRescue.Instance.HighScore.ToString();
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

    [SerializeField] private AudioClip _clickClip;

    public void ClickedPlay()
    {
        SoundManager_DotRescue.Instance.PlaySound(_clickClip);
        GameManager_DotRescue.Instance.GotoGameplay();
    }
    public void ClickedReturn()
    {
         GameManager_DotRescue.Instance.Return();
    }
   
}
