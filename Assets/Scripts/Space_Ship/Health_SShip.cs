using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_SShip : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake_SShip cameraShake;
    AudioPlayer_SShip audioPlayer;
    Scorekeeper_SShip scorekeeper;
    LevelManager_SShip levelManager;

    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake_SShip>();
        audioPlayer = FindObjectOfType<AudioPlayer_SShip>();
        scorekeeper = FindObjectOfType<Scorekeeper_SShip>();
        levelManager = FindObjectOfType<LevelManager_SShip>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer_SShip damageDealer = other.GetComponent<DamageDealer_SShip>();

        if(damageDealer != null)
        {
            
            takeDamage(damageDealer.getDamage());
            playHitEffect();
            audioPlayer.playDamageClip();
            shakeCamera();
            damageDealer.Hit();
        }
    }

    void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void playHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect
                                                    ,transform.position
                                                    ,Quaternion.identity);

            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void shakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int getHealth()
    {
        return health;
    }

    void Die()
    {
        if(!isPlayer)
        {
            scorekeeper.modifyScore(score);
        }
        else
        {
            levelManager.loadGameOver();
        }
        Destroy(gameObject);
    }
}
