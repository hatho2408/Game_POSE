
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



public class BirdController_ColorBird : MonoBehaviour
{
    private static BirdController_ColorBird instance;
    private Rigidbody2D Birdrigidbody;
    private const float JUMP_FORCE = 10f;
    private Animator anim;
    public event EventHandler onDied;
    public event EventHandler onStartPlaying;
    private BirdState state;
    public static BirdController_ColorBird Instance()
    {
        return instance;
    }
    private enum BirdState
    {
        WaitingToStart,
        Playing,
        Dead
    }

    private void Awake()
    {
        instance = this;
        Birdrigidbody = GetComponent<Rigidbody2D>();
        Birdrigidbody.bodyType = RigidbodyType2D.Static;
        state = BirdState.WaitingToStart;
         anim = GetComponent<Animator>();
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case BirdState.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
                {
                    state = BirdState.Playing;
                    Birdrigidbody.bodyType = RigidbodyType2D.Dynamic;
                    JumpEvent();
                    if(onStartPlaying!=null)onStartPlaying(this,EventArgs.Empty);
                }

                break;
            case BirdState.Playing:
                MoveEvent();
                break;
            case BirdState.Dead:
                break;
        }



    }
    private void MoveEvent()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("fly");
            JumpEvent();
            
        }
    }
    private void JumpEvent()
    {
        Birdrigidbody.velocity = Vector2.up * JUMP_FORCE;
       AudioManager_ColorBird.instance.PlaySFX(0);
       transform.eulerAngles= new Vector3(0,0,Birdrigidbody.velocity.y*0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Birdrigidbody.bodyType = RigidbodyType2D.Static;
       AudioManager_ColorBird.instance.PlaySFX(3);
        if (onDied != null) onDied(this, EventArgs.Empty);

    }
}
