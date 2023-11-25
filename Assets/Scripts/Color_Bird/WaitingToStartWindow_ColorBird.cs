using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingToStartWindow_ColorBird : MonoBehaviour
{
    private void Start() {
        BirdController_ColorBird.Instance().onStartPlaying+=WaitingToStartWindow_OnStartPlaying;
       // HideFlags();
    }

    private void WaitingToStartWindow_OnStartPlaying(object sender, EventArgs e)
    {
        Hide();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
