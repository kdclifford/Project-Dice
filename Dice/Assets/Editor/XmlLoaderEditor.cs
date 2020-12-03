using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(XmlLoader)), CanEditMultipleObjects]
public class XmlLoaderEditor : Editor
{
    const string name = "ESoundClipEnum";
    const string path = "Assets/Scripts/";
    //Width offset
    private float offset = 20;

    //Acts as a Inspector Update function
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DropAreaGUI();
        ButtonGUI();
    }

    //Makes Drag and drop area 
    public void DropAreaGUI()
    {
        XmlLoader myScript = (XmlLoader)target;
        Event evt = Event.current;
        Rect drop_area = GUILayoutUtility.GetRect(0.0f, 100.0f, GUILayout.ExpandWidth(true));

        Rect middle = drop_area;
        middle.y = drop_area.y * 0.5f;
        GUI.Box(drop_area, "Drag and Drop Sound Files Here");
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

    //Displays all the Editor buttons
    public void ButtonGUI()
    {
        XmlLoader myScript = (XmlLoader)target;

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
            Sound[] tempSound = myScript.GetComponentInParent<SoundManager>().soundClips;
            string[] names = new string[tempSound.Length];
            for (int i = 0; i < tempSound.Length; i++)
            {
                names[i] = tempSound[i].name;
            }

            EditorMethods.WriteToEnum(names, name, path);
            
        }

        if (GUILayout.Button("Clear all", GUILayout.Height(50), GUILayout.Width((Screen.width * 0.5f) - offset)))
        {
            myScript.ClearAll();
        }

        GUILayout.EndHorizontal();
    }
}


