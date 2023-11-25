using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndPoint_Pixel : MonoBehaviour
{
    private InGame_UI_Pixel inGame_UI;
    private void Start()
    {
        inGame_UI = GameObject.Find("Canvas").GetComponent<InGame_UI_Pixel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Pixel>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");

            AudioManager_Pixel.instance.PlaySFX(2);
            PlayerManager_Pixel.instance.KillPlayer();

            inGame_UI.OnLevelFinished();

            GameManager_Pixel.instance.SaveBestTime();
            GameManager_Pixel.instance.SaveCollectedFruits();
            GameManager_Pixel.instance.SaveLevelInfo();
        }
    }
}
