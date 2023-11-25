using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreWindow_ColorBird : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI ScoreText;
     [SerializeField] private TextMeshProUGUI HightScoreText;

     private void Start() {
        HightScoreText.text="HIGHTSCORE: "+Score_ColorBird.GetHighScore().ToString();
     }
    
    private void Update()
    {
        ScoreText.text = LevelManager_ColorBird.Instance().GetPipePassedCount().ToString();
    }
    
}
