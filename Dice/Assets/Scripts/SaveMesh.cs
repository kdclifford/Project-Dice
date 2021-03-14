#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

// Usage: Attach to gameobject, assign target gameobject (from where the mesh is taken), Run, Press savekey

public class SaveMesh : MonoBehaviour
{

    public KeyCode saveKey = KeyCode.F12;
    private string saveName = "Room";
   // private GameObject selectedGameObject;

    void Start()
    {
        saveName = transform.parent.name + " " + gameObject.name + "Mesh";
    }


    void Update()
    {
        if (Input.GetKeyDown(saveKey))
        {
            SaveAsset();
        }
    }

    void SaveAsset()
    {
        var mf = GetComponent<MeshFilter>();
        if (mf)
        {
            var savePath = "Assets/" + saveName + ".asset";
            Debug.Log("Saved Mesh to:" + savePath);
            AssetDatabase.CreateAsset(mf.mesh, savePath);
        }
    }
}
#endif