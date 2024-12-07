using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    List<WaveConfig> waveConfigs;
    [SerializeField]
    int startingWave = 0;
    [SerializeField]
    bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(spawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnEnemiesInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.getEnemyAmount(); i++ )
        {
            var newEnemy = Instantiate(wave.getEnemyPrefab(), wave.getWaypoints()[0].transform.position, wave.getEnemyPrefab().transform.rotation);

            EnemyPathing enemyPathing = newEnemy.GetComponent<EnemyPathing>();
            if(enemyPathing != null )
            {
                enemyPathing.WaveConfig = wave;
            }
            yield return new WaitForSeconds(wave.getTimeBetweenSpawns());
        }
    }

    private IEnumerator spawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }
    }
}
