using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI_LoadSence_MainMenu : MonoBehaviour
{
   public void LoadGameScene(string sceneName)
   {
     SceneManager.LoadScene(sceneName);
   }
}
