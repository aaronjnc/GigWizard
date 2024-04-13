using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<SpawnPoint> openSpawnPoints = new List<SpawnPoint>();
    private List<SpawnPoint> closedSpawnPoints = new List<SpawnPoint>();

    [SerializeField]
    private int spawnPointCloseTime;

    protected override void Awake()
    {
        base.Awake();
        openSpawnPoints.AddRange(FindObjectsOfType<SpawnPoint>());
    }

    public Vector3 GetSpawnLocation()
    {
        if (openSpawnPoints.Count == 0)
        {
            WaitForPoint();
        }
        int randSpawnPoint = UnityEngine.Random.Range(0, openSpawnPoints.Count - 1);
        SpawnPoint spawnPoint = openSpawnPoints[randSpawnPoint];
        openSpawnPoints.RemoveAt(randSpawnPoint);
        CloseSpawnPoint(spawnPoint);
        return spawnPoint.transform.position;
    }

    async void CloseSpawnPoint(SpawnPoint spawnPoint)
    {
        closedSpawnPoints.Add(spawnPoint);
        await Task.Delay(spawnPointCloseTime);
        closedSpawnPoints.Remove(spawnPoint);
        openSpawnPoints.Add(spawnPoint);
    }

    IEnumerator WaitForPoint()
    {
        yield return new WaitForSeconds(spawnPointCloseTime);
    }
}
