using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<SpawnPoint> openSpawnPoints = new List<SpawnPoint>();
    private List<SpawnPoint> closedSpawnPoints = new List<SpawnPoint>();

    [SerializeField]
    private float spawnPointCloseTime;

    protected override void Awake()
    {
        base.Awake();
        openSpawnPoints.AddRange(FindObjectsOfType<SpawnPoint>());
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
