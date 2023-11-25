using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone_Pixel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Pixel>() != null)
            PlayerManager_Pixel.instance.OnFalling();
    }
}
