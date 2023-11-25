using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement_SShip : MonoBehaviour
{
    [SerializeField] protected Vector3 worldPosition;
    [SerializeField] protected float speed = 7.5f;

    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingLeft;
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter_SShip shooter;

    void Awake() 
    {
        shooter = GetComponent<Shooter_SShip>();
    }

    void Start() 
    {
        InitBounds();
    }
    
    void FixedUpdate() 
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }



    void Move()
    {
        this.worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.worldPosition.z = 0;
        Vector3 newPos = Vector3.Lerp(transform.position, worldPosition, Time.fixedDeltaTime * this.speed);
        

        newPos.x = Mathf.Clamp(newPos.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = newPos;
    }
}
