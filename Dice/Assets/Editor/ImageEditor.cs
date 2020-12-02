using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ImageCreate))]
public class ImageEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ImageCreate myScript = (ImageCreate)target;
        if (GUILayout.Button("Combine", GUILayout.Height(50), GUILayout.Width((EditorGUIUtility.currentViewWidth * 0.5f))))
        {

            myScript.Combine();
        }

    }
}
