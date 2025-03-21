using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationTracker : MonoBehaviour
{
    public List<GameObject> currentObjects;
    public int spawnedObjects;
    private float framerate;
    private float highestFramerate;
    private float lowestFramerate;
    [SerializeField] private float fpsUpdateRate = 1;

    [SerializeField] private TextMeshProUGUI framerateText;
    [SerializeField] private TextMeshProUGUI lowestFramerateText;
    [SerializeField] private TextMeshProUGUI highestFramerateText;
    [SerializeField] private TextMeshProUGUI currentObjectsText;
    [SerializeField] private TextMeshProUGUI spawnedObjectsText;

    private void Start()
    {
        highestFramerate = 0f;
        lowestFramerate = Mathf.Infinity;
        StartCoroutine(UpdateFPS());
    }

    private void Update()
    {
        UpdateStats();
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            framerate = Mathf.Round(1f / Time.deltaTime);
            if (framerate > highestFramerate)
            {
                highestFramerate = framerate;
            }
            if (framerate < lowestFramerate)
            {
                lowestFramerate = framerate;
            }
            framerateText.text = "FPS: " + framerate.ToString();
            highestFramerateText.text = "Highest FPS: " + highestFramerate.ToString();
            lowestFramerateText.text = "Lowest FPS: " + lowestFramerate.ToString();
            yield return new WaitForSeconds(fpsUpdateRate);
        }        
    }

    private void UpdateStats()
    {
        currentObjectsText.text = "Current Objects: " + currentObjects.Count;
        spawnedObjectsText.text = "Spawned Objects: " + spawnedObjects.ToString();
    }

    public void DestroyAllObjects()
    {
        for (int i = 0; i < currentObjects.Count; i++)
        {
            Destroy(currentObjects[i]);
        }
        currentObjects.Clear();
    }

    public void ResetStats()
    {
        highestFramerate = 0f;
        lowestFramerate = Mathf.Infinity;
        spawnedObjects = 0;
    }
}
