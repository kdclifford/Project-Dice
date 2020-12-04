using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMeshTest : MonoBehaviour
{

    public List<MeshFilter> newchildFilters;
    public MeshFilter[] childFilters;
    // Start is called before the first frame update
    void Start()
    {
        childFilters = new MeshFilter[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childFilters[i] = transform.GetChild(i).GetComponent<MeshFilter>();
        }

        //GetComponent<MeshFilter>().
        GetComponent<MeshFilter>().sharedMesh = CombineMeshes(childFilters);
    }

    // Update is called once per frame
    void Update()
    {
     
    }


   private Mesh CombineMeshes(MeshFilter[] meshes)
    {
        // Key: shared mesh instance ID, Value: arguments to combine meshes
        var helper = new Dictionary<int, List<CombineInstance>>();

        // Build combine instances for each type of mesh
        foreach (var m in meshes)
        {
            List<CombineInstance> tmp;
            if (!helper.TryGetValue(m.sharedMesh.GetInstanceID(), out tmp))
            {
                tmp = new List<CombineInstance>();
                helper.Add(m.sharedMesh.GetInstanceID(), tmp);
            }

            var ci = new CombineInstance();
            ci.mesh = m.sharedMesh;
            ci.transform = m.transform.localToWorldMatrix;
            tmp.Add(ci);
        }

        // Combine meshes and build combine instance for combined meshes
        var list = new List<CombineInstance>();
        foreach (var e in helper)
        {
            var m = new Mesh();
            m.CombineMeshes(e.Value.ToArray());
            var ci = new CombineInstance();
            ci.mesh = m;
            list.Add(ci);
        }

        // And now combine everything
        var result = new Mesh();
        result.CombineMeshes(list.ToArray(), false, false);

        // It is a good idea to clean unused meshes now
        foreach (var m in list)
        {
            Destroy(m.mesh);
        }

        return result;

    }


}
