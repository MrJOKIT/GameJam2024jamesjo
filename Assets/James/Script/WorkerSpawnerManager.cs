using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorkerSpawnerManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    [SerializeField] private int workerCountToSpawn;
    [SerializeField] private List<GameObject> workerSpawn;
    private bool canSpawn = true;

    [Header("Worker Type")] 
    public GameObject[] workerPrefab;

    private void Start()
    {
        SetUpSpawner();
        canSpawn = true;
        
    }

    private void SetUpSpawner()
    {
        for (int i = 0; i < workerCountToSpawn; i++)
        {
            workerSpawn.Add(workerPrefab[Random.Range(0,workerPrefab.Length )]);
        }
    }

    private void Update()
    {
        RandomSpawn();
    }

    private void RandomSpawn()
    {
        if (workerCountToSpawn > 0 && canSpawn)
        {
            StartCoroutine(SpawnWorker());
            workerCountToSpawn -= 1;
        }
        
    }

    IEnumerator SpawnWorker()
    {
        canSpawn = false;
        int spawnNumber = Random.Range(0, spawnPoint.Length);
        Instantiate(workerSpawn[0],spawnPoint[spawnNumber].position,spawnPoint[spawnNumber].rotation);
        workerSpawn.Remove(workerSpawn[0]);
        Debug.Log($"Spawn way{spawnNumber}");
        yield return new WaitForSeconds(1f);
        canSpawn = true;
    }
}
