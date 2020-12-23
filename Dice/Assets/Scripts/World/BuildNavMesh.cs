using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<NavMeshSurface>();
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
