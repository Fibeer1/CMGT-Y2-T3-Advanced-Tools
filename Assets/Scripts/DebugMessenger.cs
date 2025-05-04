using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMessenger : MonoBehaviour
{
    //Singleton designed to send debug messages with added functionality of being turned on/off
    //Easier to use than commenting all debug messages in other scripts when they are not needed
    //Also important to mention this class is only used for normal messages, not error messages

    public static DebugMessenger Instance;
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
