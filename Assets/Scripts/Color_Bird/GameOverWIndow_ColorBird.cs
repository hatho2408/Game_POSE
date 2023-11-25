using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
using System;
using JetBrains.Annotations;

public class GameOverWIndow_ColorBird : MonoBehaviour
{
    private static GameOverWIndow_ColorBird instance;

    public static GameOverWIndow_ColorBird Instance()
    {
        return instance;
    }
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
     [SerializeField] private TextMeshProUGUI hightScoreTextMesh;
    [SerializeField] private GameObject GOWindow;
    private void Awake()
    {    instance=this;
          Hide();
    }

 
    private void Hide()
    {
        GOWindow.SetActive(false);
    }
    public void Show()
    {
        scoreTextMesh.text = "SCORE: "+LevelManager_ColorBird.Instance().GetPipePassedCount().ToString();
         hightScoreTextMesh.text="HIGHTSCORE: "+Score_ColorBird.GetHighScore().ToString();
        GOWindow.SetActive(true);
    }
    public void onRetryButton()
    {
        AudioManager_ColorBird.instance.PlaySFX(1);
        Loader_ColorBird.Load(Loader_ColorBird.Scene.Color_Bird_GameScene);
    }
    public void MainMenuButton()
    {
        AudioManager_ColorBird.instance.PlaySFX(1);
         Loader_ColorBird.Load(Loader_ColorBird.Scene.Color_Bird_MainMenuScene);
    }
     
}
