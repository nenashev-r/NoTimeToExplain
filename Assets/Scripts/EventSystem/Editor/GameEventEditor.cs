using EventSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent gameEvent = (GameEvent)target;
        if (GUILayout.Button("Rise event"))
        {
            gameEvent.Invoke();
        }
    }
}
