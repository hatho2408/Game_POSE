using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuWIndow_ColorBird : MonoBehaviour
{

    public void PlayGameBtn()
    {
        AudioManager_ColorBird.instance.PlaySFX(1);
        Loader_ColorBird.Load(Loader_ColorBird.Scene.Color_Bird_GameScene);
    }
    public void QuitBtn()
    {
        AudioManager_ColorBird.instance.PlaySFX(1);
        Application.Quit();
    }
    public void ReturnBTN()
    {
         Loader_ColorBird.Load(Loader_ColorBird.Scene.MainMenuGame);

    }


}
