using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config Oscillate")]
public class OscillatingEnemyWaveConfig : ScriptableObject
{  
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject pathPrefab;
    [SerializeField]
    float timeBetweenSpawns = 0.5f;
    [SerializeField]
    float spawnRandomFactor = 0.25f;
    [SerializeField]
    int enemyNumbers = 20;
    [SerializeField]
    float moveSpeed = 5f;

    public GameObject getEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> getSpawnpoint()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float getTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float getSpawnRandom()
    {
        return spawnRandomFactor;
    }

    public int getEnemyAmount()
    {
        return enemyNumbers;
    }

    public float getEnemySpeed()
    {
        return moveSpeed;
    }
}
