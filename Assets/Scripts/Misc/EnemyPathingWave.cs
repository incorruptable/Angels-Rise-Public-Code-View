using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathingWave : MonoBehaviour
{
    WaveConfig waveConfig;
    private List<Transform> waypoints;
    private Vector2 startPosition;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        waypoints = waveConfig.getWaypoints();
        startPosition = waypoints[waypointIndex].transform.position;
        transform.position = startPosition + new Vector2(0f, Mathf.Sin(Time.time));
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var movementSpeed = waveConfig.getEnemySpeed() * Time.deltaTime;
        transform.forward = startPosition + new Vector2(movementSpeed, Mathf.Sin(Time.time));
    }

    public void SetWaveConfig(WaveConfig waveInfo)
    {
        waveConfig = waveInfo;
    }
}
