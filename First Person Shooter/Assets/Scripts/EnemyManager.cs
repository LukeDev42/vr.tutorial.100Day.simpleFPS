using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int EnemiesPerWave;
    public GameObject Enemy;
}


public class EnemyManager : MonoBehaviour {

    public Wave[] Waves;
    public Transform[] SpawnPoints;
    public float TimeBetweenEnemies = 2f;

    private GameManager _gameManager;

    private int _totalEnemiesInCurrentWave;
    private int _enemiesInWaveLeft;
    private int _spawnedEnemies;

    private int _currentWave;
    private int _totalWaves;

    private void Start()
    {
        _gameManager = GetComponentInParent<GameManager>();

        _currentWave = -1;
        _totalWaves = Waves.Length - 1;

        StartNextWave();
    }

    void StartNextWave()
    {
        _currentWave++;

        Debug.Log("Current Wave: " + _currentWave);
        Debug.Log("Total Waves: " + _totalWaves);
        
        if(_currentWave > _totalWaves)
        {
            _gameManager.Victory();
            return;
        }

        _totalEnemiesInCurrentWave = Waves[_currentWave].EnemiesPerWave;
        _enemiesInWaveLeft = 0;
        _spawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        GameObject enemy = Waves[_currentWave].Enemy;
        while (_spawnedEnemies < _totalEnemiesInCurrentWave)
        {
            _spawnedEnemies++;
            _enemiesInWaveLeft++;

            int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

            Instantiate(enemy, SpawnPoints[spawnPointIndex].position,
                SpawnPoints[spawnPointIndex].rotation);
            yield return new WaitForSeconds(TimeBetweenEnemies);
        }
        yield return null;
    }

    public void EnemyDefeated()
    {
        _enemiesInWaveLeft--;

        if(_enemiesInWaveLeft == 0 && _spawnedEnemies ==_totalEnemiesInCurrentWave)
        {
            StartNextWave();
        }
    }
}
