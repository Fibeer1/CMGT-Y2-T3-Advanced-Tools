using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(ObjectSpawner))]
public class ObjectSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox("SpawnStopCount is used to make the " +
            "spawner stop after spawning objects a certain number of times.\n" +
            "If SpawnStopCount is less than 1, the spawner will not stop working", MessageType.Info);
        EditorGUILayout.HelpBox("Seed is used to keep spawn positions consistent", MessageType.Info);
    }
}
