using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpellList))]
public class SpellEnumEditor : Editor
{    
    const string name = "ESpellEnum";
    const string path = "Assets/Scripts/Spell/";
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ButtonShow();
    }

    void ButtonShow()
    {
        SpellList myScript = (SpellList)target;
        if (GUILayout.Button("CreateEnum", GUILayout.Height(50), GUILayout.Width((Screen.width * 0.5f))))
        {

            myScript.InitializeSpells();
           
            string[] names = new string[myScript.spells.Count];
            for (int i = 0; i < myScript.spells.Count; i++)
            {
                names[i] = myScript.spells[i].ToString() ;
            }

            EditorMethods.WriteToEnum(names, name, path);


        }
    }
}
