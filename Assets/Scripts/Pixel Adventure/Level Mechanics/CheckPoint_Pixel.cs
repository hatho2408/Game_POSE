using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Pixel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Pixel>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");
            PlayerManager_Pixel.instance.respawnPoint = transform;
        }
    }
}
