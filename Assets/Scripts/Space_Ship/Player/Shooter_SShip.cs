using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_SShip : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool enemyAutoFire;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float miniumFiringRate = 0.1f;
    [HideInInspector] public bool isAutoFire = true;
    Coroutine firingCoroutine;
    AudioPlayer_SShip audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer_SShip>();
    }
    void Start()
    {   
        if(enemyAutoFire)
        {
            isAutoFire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isAutoFire && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isAutoFire && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
            isAutoFire = false;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab
                                                ,transform.position
                                                ,Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = UnityEngine.Random.Range(baseFiringRate - firingRateVariance,
                                                                    baseFiringRate + firingRateVariance);
            
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, miniumFiringRate, float.MaxValue);

            audioPlayer.playShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
            
        }
    }
}
