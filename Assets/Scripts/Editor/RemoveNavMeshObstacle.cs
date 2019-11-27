using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RemoveComponent))]
public class RemoveNavMeshObstacle : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RemoveComponent rmc = (RemoveComponent)target;

        if (GUILayout.Button("Remove ComponentName"))
        {
            rmc.RemoveComponents();
        }
    }
}
