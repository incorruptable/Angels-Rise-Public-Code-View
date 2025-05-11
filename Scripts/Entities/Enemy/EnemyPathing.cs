using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig waveConfig;
    private List<Transform> waypoints;
    private int waypointIndex = 0;

    private float enemySpeed;

    public WaveConfig WaveConfig
    {
        get { return waveConfig; }
        set { waveConfig = value; }
    }


    public void Initialize()
    {
        if (waveConfig != null)
        {
            waypoints = waveConfig.getWaypoints();
            transform.position = waypoints[0].position;
            enemySpeed = waveConfig.getEnemySpeed();
        }
    }

    public void HandlePathing()
    {
        if (waypoints == null || waypoints == null || waypointIndex >= waypoints.Count) return;
        var targetPosition = waypoints[waypointIndex].position;
        var movementThisFrame = waveConfig.getEnemySpeed() * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
        if (transform.position == targetPosition)
        {
            waypointIndex++;
        }

        if (waypointIndex >= waypoints.Count) Destroy(gameObject, 2f);
    }
}
