using HeneGames.DialogueSystem;
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
    public DialogueManager waveEndDialogue;
}

public class EnemyManager : Singleton<EnemyManager>
{

    [SerializeField]
    private List<WaveInformation> waves;

    private List<GameObject> activeEnemies;

    private GameObject player;

    int currentWave = 0;

    private List<SpawnPoint> openSpawnPoints = new List<SpawnPoint>();
    private List<SpawnPoint> closedSpawnPoints = new List<SpawnPoint>();
    private DialogueTrigger playerTrigger;
        
    [SerializeField]
    private float spawnPointCloseTime;

    private void Start()
    {
        openSpawnPoints.AddRange(FindObjectsOfType<SpawnPoint>());
        activeEnemies = new List<GameObject>();
        player = PlayerMovement.Instance.gameObject;
        playerTrigger = player.GetComponent<DialogueTrigger>();
    }

    public void StartWaves()
    {
        NewWave();
    }

    public void NextWave()
    {
        currentWave++;
        NewWave();
    }

    private void NewWave()
    {
        PlayerCharacter.Instance.RegainMana();
        if (currentWave >= waves.Count)
        {
            GameManager.Instance.WinGame();
            return;
        }
        WaveInformation wave = waves[currentWave];
        for (int i = 0; i < wave.enemyCount; i++)
        {
            int randEnemy = UnityEngine.Random.Range(0, wave.enemyPrefabs.Length - 1);
            GameObject newEnemy = Instantiate(wave.enemyPrefabs[randEnemy], GetSpawnLocation(), Quaternion.identity);
            EnemyCharacter enemyCharacter = newEnemy.GetComponent<EnemyCharacter>();
            if (UnityEngine.Random.Range(0,2) == 0)
            {
                enemyCharacter.SetTarget(PlayerMovement.Instance.gameObject);
            }
            else
            {
                enemyCharacter.SetTarget(Flower.Instance.gameObject);
            }
            activeEnemies.Add(newEnemy);
        }
    }

    public void RemoveEnemy(GameObject killedEnemy)
    {
        activeEnemies.Remove(killedEnemy);
        if (activeEnemies.Count == 0)
        {
            PlayerCharacter.Instance.transform.position = new Vector3(Flower.Instance.transform.position.x, PlayerCharacter.Instance.transform.position.y, Flower.Instance.transform.position.z - 1.0f);
            waves[currentWave].waveEndDialogue.TriggerDialogue(playerTrigger);
        }
    }

    public Vector3 GetSpawnLocation()
    {
        if (openSpawnPoints.Count == 0)
        {
            openSpawnPoints.AddRange(closedSpawnPoints.ToArray());
            closedSpawnPoints.Clear();
        }
        int randSpawnPoint = UnityEngine.Random.Range(0, openSpawnPoints.Count - 1);
        SpawnPoint spawnPoint = openSpawnPoints[randSpawnPoint];
        openSpawnPoints.RemoveAt(randSpawnPoint);
        closedSpawnPoints.Add(spawnPoint);
        return spawnPoint.transform.position;
    }
}
