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
            SpellList.instance.InitializeSpells();
            myScript.LoadUI();
        }
    }
}
