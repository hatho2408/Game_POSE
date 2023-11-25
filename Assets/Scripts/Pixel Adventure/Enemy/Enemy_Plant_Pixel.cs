using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Plant_Pixel : Enemy_Pixel
{
    [Header("Plant specific")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool facingRight;

    
    protected override void Start()
    {
        base.Start();
        if (facingRight)
            Flip();
    }

    void Update()
    {
        CollisionChecks();
        idleTimeCounter -= Time.deltaTime;

        if (!playerDetection)
            return;
        
        bool playerDetected = playerDetection.collider.GetComponent<Player_Pixel>() != null;

        if (idleTimeCounter < 0 && playerDetected)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("attack");
        }
    }

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
        newBullet.GetComponent<Bullet_Pixel>().SetupSpeed(bulletSpeed * facingDirection, 0);
        Destroy(newBullet, 3f);
    }
}
