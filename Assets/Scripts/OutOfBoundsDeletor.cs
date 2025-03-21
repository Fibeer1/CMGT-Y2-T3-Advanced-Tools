using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsDeletor : MonoBehaviour
{
    private InformationTracker tracker;

    private void Start()
    {
        tracker = FindObjectOfType<InformationTracker>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (tracker != null && tracker.currentObjects.Contains(other.gameObject))
        {
            tracker.currentObjects.Remove(other.gameObject);
        }
        Destroy(other.gameObject);
    }
}
