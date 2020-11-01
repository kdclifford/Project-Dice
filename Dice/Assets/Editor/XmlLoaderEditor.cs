using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(XmlLoader)), CanEditMultipleObjects]
public class XmlLoaderEditor : Editor
{
    private SerializedObject obj;
    float offset = 20;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        DropAreaGUI();

        XmlLoader myScript = (XmlLoader)target;

        //GUILayout.BeginHorizontal(

        // Starts a horizontal group
        GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Add Sound to List", GUILayout.Height(50), GUILayout.Width((EditorGUIUtility.currentViewWidth * 0.5f) - offset)))
        {

            myScript.AddSoundToList();
        }

        if (GUILayout.Button("Load XML File", GUILayout.Height(50), GUILayout.Width((EditorGUIUtility.currentViewWidth * 0.5f) - offset)))
        {
            myScript.LoadXml();
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box", GUILayout.Width((Screen.width) - offset * 2));
        
        if (GUILayout.Button("Save all Changes to XML", GUILayout.Height(50), GUILayout.Width((Screen.width * 0.5f) - offset)))
        {
            myScript.SaveXML();
        }

        if (GUILayout.Button("Clear all", GUILayout.Height(50), GUILayout.Width((Screen.width * 0.5f) - offset)))
        {
            myScript.ClearAll();
        }

        GUILayout.EndHorizontal();
    }



    public void OnEnable()
    {
        obj = new SerializedObject(target);
    }  

    public void DropAreaGUI()
    {
        XmlLoader myScript = (XmlLoader)target;
        Event evt = Event.current;
        Rect drop_area = GUILayoutUtility.GetRect(0.0f, 100.0f, GUILayout.ExpandWidth(true));
        //GUI.Box(drop_area, "Add Sound Files.");

        Rect middle = drop_area;
        middle.y = drop_area.y * 0.5f;
        GUI.Box(drop_area, "Drag and Drop Sound Files");        

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!drop_area.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (AudioClip dragged_object in DragAndDrop.objectReferences)
                    {
                        // Do On Drag Stuff here
                        myScript.AddXML(dragged_object);
                    }
                }
                break;
        }
    }





}


