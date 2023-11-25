using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint_Pixel : MonoBehaviour
{
    [SerializeField] private Transform respPoint;

    private void Awake()
    {
        
    }
    private void Start()
    {
        PlayerManager_Pixel.instance.respawnPoint = respPoint;
        PlayerManager_Pixel.instance.RespawnPlayer();

        PlayerManager_Pixel.instance.fruits = 0;
        GameManager_Pixel.instance.timer = 0;

        AudioManager_Pixel.instance.PlayRandomBGM();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Pixel>() != null)
        {
            if (!GameManager_Pixel.instance.startTime)
                GameManager_Pixel.instance.startTime = true;

            if(collision.transform.position.x > transform.position.x)
                GetComponent<Animator>().SetTrigger("touch");
        }   
    }
}
