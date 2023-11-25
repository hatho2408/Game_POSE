using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Pixel : MonoBehaviour
{
    // this one responsible for giving damage to the player

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Pixel>() != null)
        {
            Player_Pixel player = collision.GetComponent<Player_Pixel>();

            player.Knockback(transform);
        }
            
    }
}
