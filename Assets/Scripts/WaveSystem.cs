using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSystem : MonoBehaviour
{

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemySpawner;
    private int currentWaveIndex = -1;

    public int CurrentWaveIndex => currentWaveIndex + 1;
    public int MaxWave => waves.Length;


    public void StartWave()
    {
        if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;

            if(currentWaveIndex == waves.Length - 1)
            {
                enemySpawner.StartWave(waves[currentWaveIndex], true);

            }
            else
            {
                enemySpawner.StartWave(waves[currentWaveIndex], false);

            }

        }
        //else if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1)
        //{
        //    gameController.GameOver();
        //}

    }
}

[System.Serializable]
public class Wave
{
    public float spawnTime; // interval of enemy spawn of this wave
    public int[] maxEnemyCount; // number of enemy spawn in this wave
    public GameObject[] enemyPrefabs; // the types of enemies spawning in this wave
}
