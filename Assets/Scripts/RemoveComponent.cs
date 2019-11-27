using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RemoveComponent : MonoBehaviour
{
    public void RemoveComponents()
    {
        Component[] components = GetComponentsInChildren(typeof(NavMeshObstacle), true);

        foreach (var c in components)
        {
            DestroyImmediate(c);
        }
    }
}
