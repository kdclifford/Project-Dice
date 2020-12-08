﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MeshCombine : MonoBehaviour
{
    private MeshFilter[] childFilters;
    //private MeshRenderer[] childRenderers;

    Mesh mergedMesh;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshFilter>().sharedMesh = AdvancedMerge();
        BasicMerge();
    }


    // Update is called once per frame
    void BasicMerge()
    {
        CheckForMeshComps();
        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        mergedMesh = new Mesh();
        childFilters = GetComponentsInChildren<MeshFilter>();
        Material material = transform.GetChild(1).GetComponent<MeshRenderer>().material;

        CombineInstance[] combineInstances = new CombineInstance[childFilters.Length];

        for (int i = 0; i < childFilters.Length; i++)
        {
            if (childFilters[i].transform == transform)
            {
                continue;
            }

            combineInstances[i].subMeshIndex = 0;
            combineInstances[i].mesh = childFilters[i].sharedMesh;
            combineInstances[i].transform = childFilters[i].transform.localToWorldMatrix;
            //transform.GetChild(i).gameObject.SetActive(false);
        }

        mergedMesh.CombineMeshes(combineInstances);
        mergedMesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mergedMesh;
        transform.localScale *= 0.33333f;
        GetComponent<MeshRenderer>().sharedMaterial = material;
        gameObject.AddComponent<MeshCollider>();

        transform.position = oldPos;
        transform.rotation = oldRot;

        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).gameObject.SetActive(false);
            Destroy(transform.GetChild(i).gameObject);
        }
        
    }

    void CheckForMeshComps()
    {
        if (GetComponent<MeshFilter>() == null)
        {
            gameObject.AddComponent<MeshFilter>();
        }

        if (GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
        }
    }


    public Mesh AdvancedMerge()
    {
        // All our children (and us)
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(true);

        // All the meshes in our children (just a big list)
        List<Material> materials = new List<Material>();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(true); // <-- you can optimize this
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.transform == transform)
                continue;
            Material[] localMats = renderer.sharedMaterials;
            foreach (Material localMat in localMats)
                if (!materials.Contains(localMat))
                    materials.Add(localMat);
        }

        // Each material will have a mesh for it.
        List<Mesh> submeshes = new List<Mesh>();
        foreach (Material material in materials)
        {
            // Make a combiner for each (sub)mesh that is mapped to the right material.
            List<CombineInstance> combiners = new List<CombineInstance>();
            foreach (MeshFilter filter in filters)
            {
                if (filter.transform == transform) continue;
                // The filter doesn't know what materials are involved, get the renderer.
                MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)
                if (renderer == null)
                {
                    Debug.LogError(filter.name + " has no MeshRenderer");
                    continue;
                }

                // Let's see if their materials are the one we want right now.
                Material[] localMaterials = renderer.sharedMaterials;
                for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
                {
                    if (localMaterials[materialIndex] != material)
                        continue;
                    // This submesh is the material we're looking for right now.
                    CombineInstance ci = new CombineInstance();
                    ci.mesh = filter.sharedMesh;
                    ci.subMeshIndex = materialIndex;
                    ci.transform = Matrix4x4.identity;
                    combiners.Add(ci);
                }
            }
            // Flatten into a single mesh.
            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combiners.ToArray(), true);
            submeshes.Add(mesh);
        }

        // The final mesh: combine all the material-specific meshes as independent submeshes.
        List<CombineInstance> finalCombiners = new List<CombineInstance>();
        foreach (Mesh mesh in submeshes)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = mesh;
            ci.subMeshIndex = 0;
            ci.transform = Matrix4x4.identity;
            finalCombiners.Add(ci);
        }
        Mesh finalMesh = new Mesh();
        finalMesh.CombineMeshes(finalCombiners.ToArray(), false);
        return finalMesh;

       // myMeshFilter.sharedMesh = finalMesh;
        Debug.Log("Final mesh has " + submeshes.Count + " materials.");
    }


}


