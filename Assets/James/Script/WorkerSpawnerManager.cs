using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorkerSpawnerManager : MonoBehaviour
{
    public static WorkerSpawnerManager instance;
    [SerializeField] private float spawnDelay;
    public Transform[] spawnPoint;
    [SerializeField] private int workerCountToSpawn;
    [SerializeField] private List<GameObject> workerSpawn;
    private bool canSpawn = true;

    [Header("Worker Type")] 
    public GameObject[] workerPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetUpSpawner();
        canSpawn = true;
        
    }

    public void SetUpSpawner()
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
        if (workerCountToSpawn > 0 && canSpawn && GameManager.instance.onTankSpawn == false && GameManager.instance.isGameOver == false)
        {
            StartCoroutine(SpawnWorker());
            
        }
        
    }

    IEnumerator SpawnWorker()
    {
        canSpawn = false;
        int spawnNumber = Random.Range(0, spawnPoint.Length);
        Instantiate(workerSpawn[0],spawnPoint[spawnNumber].position,spawnPoint[spawnNumber].rotation);
        workerCountToSpawn -= 1;
        workerSpawn.Remove(workerSpawn[0]);
        //Debug.Log($"Spawn way{spawnNumber}");
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

    public void AddWorker(int count)
    {
        workerCountToSpawn += count;
    }
}
