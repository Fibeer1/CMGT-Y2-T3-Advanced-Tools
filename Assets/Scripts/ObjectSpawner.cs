using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnCubeSize;
    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private int prefabIndexToSpawn;
    [SerializeField] private bool randomizeIndexAfterEachSpawn = true;

    [SerializeField] private float spawnTimer;
    [SerializeField] private float spawnTime;
    
    [SerializeField] private int objectCountPerSpawn = 1;

    public bool shouldSpawn = true;

    private InformationTracker tracker;

    private void Start()
    {
        tracker = FindObjectOfType<InformationTracker>();
    }

    private void Update()
    {
        if (!shouldSpawn)
        {
            return;
        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < objectCountPerSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomPointInCube();
            GameObject objectInstance = Instantiate(prefabs[prefabIndexToSpawn], spawnPosition, Quaternion.identity, transform);
            tracker.currentObjects.Add(objectInstance);
            tracker.spawnedObjects++;
        }
        if (randomizeIndexAfterEachSpawn)
        {
            prefabIndexToSpawn = Random.Range(0, prefabs.Length);
        }
        spawnTimer = spawnTime;
    }

    private Vector3 GetRandomPointInCube()
    {
        Vector3 halfSize = spawnCubeSize * 0.5f;
        return new Vector3(
            Random.Range(-halfSize.x, halfSize.x),
            Random.Range(-halfSize.y, halfSize.y), 
            Random.Range(-halfSize.z, halfSize.z)) + transform.position;
    }

    public void ToggleObjectSpawner()
    {
        shouldSpawn = !shouldSpawn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnCubeSize);
    }
}
