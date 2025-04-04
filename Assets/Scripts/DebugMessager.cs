using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMessager : MonoBehaviour
{
    public static DebugMessager Instance;
    [SerializeField] private bool showDebugMessages;

    private void Awake()
    {
        Instance = this;
    }

    public static void DebugMessage(string message)
    {
        if (Instance == null || (Instance != null && !Instance.showDebugMessages))
        {
            return;
        }
        Debug.Log(message);
    }
}
