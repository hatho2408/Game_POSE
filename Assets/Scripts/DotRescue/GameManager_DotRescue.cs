using UnityEngine;

public class GameManager_DotRescue : MonoBehaviour
{
    public static GameManager_DotRescue Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private string highScoreKey = "HighScore";

    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(highScoreKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(highScoreKey, value);
        }
    }

    public int CurrentScore
    { get; set; }

    public bool IsInitialized
    { get; set; }


    private void Init()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }

    private const string MainMenu = "DotRescue_MainMenu";
    private const string Gameplay = "DotRescue_Gameplay";
    private const string ReturnMenu="MainMenuGame";

    public void GotoMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenu);
    }

    public void GotoGameplay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Gameplay);
    }
    public void Return()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(ReturnMenu);
    }
}
