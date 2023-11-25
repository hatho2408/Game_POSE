using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader_ColorBird
{
    public enum Scene
    {
        Color_Bird_GameScene,
        Color_Bird_LoadingScene,
        Color_Bird_MainMenuScene,
        MainMenuGame,
    }
    private static Scene targetScene;
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.Color_Bird_LoadingScene.ToString());
         targetScene=scene;
        

    }
    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}

