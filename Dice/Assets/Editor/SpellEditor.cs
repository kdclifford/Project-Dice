using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ProjectileType))]
public class SpellEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ProjectileType myScript = (ProjectileType)target;
        if ((int)myScript.spellIndex > -1)
        {
            myScript.setSpell();
            myScript.spellList.InitializeSpells();
            myScript.LoadUI();
        }


    }
}
