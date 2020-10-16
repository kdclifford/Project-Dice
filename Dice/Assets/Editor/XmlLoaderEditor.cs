using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(XmlLoader)), CanEditMultipleObjects]
// ^ This is the script we are making a custom editor for.
public class XmlLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        XmlLoader myScript = (XmlLoader)target;

        //GUILayout.BeginHorizontal(

        // Starts a horizontal group
        GUILayout.BeginHorizontal("box");

        if ( GUILayout.Button("Add Sound to List", GUILayout.Height(50)))
        {
            
            myScript.AddSoundToList();
        }

        if (GUILayout.Button("Load XML File", GUILayout.Height(50)))
        {
            myScript.LoadXml();
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Save all Changes to XML", GUILayout.Height(50)))
        {
            myScript.SaveXML();
        }
    }
}
