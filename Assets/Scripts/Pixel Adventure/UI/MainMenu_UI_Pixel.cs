using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI_Pixel : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private VolumeController_UI_Pixel[] volumeController;

    private void Start()
    {
        bool showButton = PlayerPrefs.GetInt("Level" + 2 + "Unlocked") == 1;
        continueButton.SetActive(showButton);


        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<VolumeController_UI_Pixel>().SetupVolumeSlider();
        }

        AudioManager_Pixel.instance.PlayBGM(0);


        //PlayerPrefs.SetInt("SkinPurchased" + 3, 0);
        //PlayerPrefs.SetInt("TotalFruitsCollected", 1248);
    }
    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        AudioManager_Pixel.instance.PlaySFX(4);
        uiMenu.SetActive(true);
    }

    public void SetGameDifficulty(int i) => GameManager_Pixel.instance.difficulty = i;

}
