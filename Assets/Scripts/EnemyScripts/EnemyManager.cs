using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct WaveInformation
{
    public int waveNumber;
    public int enemyCount;
    public GameObject[] enemyPrefabs;
    public Sprite flowerSprite;
}

public class EnemyManager : Singleton<EnemyManager>
{

    [SerializeField]
    private List<WaveInformation> waves;

    private List<GameObject> activeEnemies;

    private GameObject player;

    int currentWave = 0;

    private void Start()
    {
        activeEnemies = new List<GameObject>();
        player = PlayerMovement.Instance.gameObject;
    }

    private void NewWave()
    {
        WaveInformation wave = waves[currentWave];
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(wave.enemyPrefabs[UnityEngine.Random.Range(0, wave.enemyPrefabs.Length - 1)]);
            newEnemy.GetComponent<EnemyMovement>().MoveTo(player);
            activeEnemies.Add(newEnemy);
        }
    }

    public void RemoveEnemy(GameObject killedEnemy)
    {
        activeEnemies.Remove(killedEnemy);
        if (activeEnemies.Count == 0)
        {
            currentWave++;
            NewWave();
        }
    }
}
