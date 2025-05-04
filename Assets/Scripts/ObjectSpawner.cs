using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private int spawnStopCount = 0;
    [SerializeField] private int spawnsPerformed = 0;

    public bool shouldSpawn = true;

    [SerializeField] private int seed = 0;
    private System.Random random;
    private int spawnCounter = 0; //Used to keep the spawn positions for each object consistent

    private InformationTracker tracker;

    private void Start()
    {
        tracker = FindObjectOfType<InformationTracker>();
        //Initialize the random generator with a seed if applicable
        InitializeSeed();
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
        SpawnObjectGroup(prefabs[prefabIndexToSpawn], objectCountPerSpawn);
        if (randomizeIndexAfterEachSpawn)
        {
            prefabIndexToSpawn = random.Next(0, prefabs.Length);
        }
        spawnsPerformed++;
        if (spawnsPerformed == spawnStopCount)
        {
            shouldSpawn = false;
        }
        spawnCounter = 0;
        spawnTimer = spawnTime;
    }

    public void SpawnObjectsForTest(GameObject prefab, int objectsToSpawn)
    {
        shouldSpawn = false;
        spawnCounter = 0;
        DestroyAllObjects();
        SpawnObjectGroup(prefab, objectsToSpawn);
    }

    private void SpawnObjectGroup(GameObject prefab, int objectsToSpawn)
    {
        for (int i = 0; i < objectsToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomPointInCube();
            GameObject objectInstance = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
            tracker.currentObjects.Add(objectInstance);
            tracker.spawnedObjects++;
        }
    }

    public void DestroyAllObjects()
    {
        for (int i = 0; i < tracker.currentObjects.Count; i++)
        {
            Destroy(tracker.currentObjects[i]);
        }
        tracker.currentObjects.Clear();
    }

    private Vector3 GetRandomPointInCube()
    {
        //Create a new Random instance for each spawn, ensuring consistency
        System.Random positionRandom = new System.Random(seed + spawnCounter);
        spawnCounter++;

        Vector3 halfSize = spawnCubeSize * 0.5f;
        return new Vector3(
            (float)(positionRandom.NextDouble() * spawnCubeSize.x - halfSize.x),
            (float)(positionRandom.NextDouble() * spawnCubeSize.y - halfSize.y),
            (float)(positionRandom.NextDouble() * spawnCubeSize.z - halfSize.z)) + transform.position;
    }

    public void ToggleObjectSpawner()
    {
        shouldSpawn = !shouldSpawn;
    }

    private void OnDrawGizmos()
    {
        //Draw the cube from which the objects spawn
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnCubeSize);
    }

    private void InitializeSeed()
    {
        if (seed == 0)
        {
            seed = Random.Range(int.MinValue, int.MaxValue);
        }
        random = new System.Random(seed);
        spawnCounter = 0; //Reset spawn counter when changing seed
        DebugMessenger.DebugMessage("Seed initialized: " + seed);
    }

    private void OnValidate()
    {
        //If the seed is changed through the inspector at runtime
        InitializeSeed();
    }
}
