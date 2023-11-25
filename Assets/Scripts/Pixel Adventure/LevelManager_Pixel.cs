using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager_Pixel : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private bool[] levelOpen;


    private void Start()
    {
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);

        AssignLevelBooleans();

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i])
                 return;


            string sceneName = "Level " + i;

            GameObject newButton = Instantiate(levelButton, levelButtonParent);
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            newButton.GetComponent<LevelButton_UI_Pixel>().UpdateTextInfo(i);
        }
    }

    private void AssignLevelBooleans()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (unlocked)
                levelOpen[i] = true;
            else
                return;
        }
    }

    public void LoadLevel(string sceneName)
    {
        AudioManager_Pixel.instance.PlaySFX(4);
        GameManager_Pixel.instance.SaveGameDifficulty();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNewGame()
    {
        AudioManager_Pixel.instance.PlaySFX(4);

        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (unlocked)
                PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
            else
                SceneManager.LoadScene("Level 1");
                return;
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuGame" );
    }

    public void LoadContinueGame()
    {
        AudioManager_Pixel.instance.PlaySFX(4);

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (!unlocked)
            {
                SceneManager.LoadScene("Level " + (i - 1));
                return;
            }
        }
    }

}
