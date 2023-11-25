using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder_SShip : MonoBehaviour
{
    EnemySpawner_SShip enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake() 
    {
        enemySpawner = FindObjectOfType<EnemySpawner_SShip>();
    }
    void Start()
    {
        waveConfig = enemySpawner.getCurrentWave();
        waypoints = waveConfig.getWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }
    void Update()
    {
        followPath();
    }

    void followPath()
    {
        if(waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.getMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
