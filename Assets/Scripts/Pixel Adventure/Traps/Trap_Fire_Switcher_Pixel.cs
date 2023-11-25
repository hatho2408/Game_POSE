using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Switcher_Pixel : MonoBehaviour
{
    private Trap_Fire_Pixel myTrap;
    private Animator anim;


    [SerializeField] private float timeNotActive = 2;

    private void Start()
    {
        anim = GetComponent<Animator>();
        myTrap = GetComponentInChildren<Trap_Fire_Pixel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Pixel>() != null)
        {
            anim.SetTrigger("pressed");
            myTrap.FireSwitchAfter(timeNotActive);
        }
    }
}
