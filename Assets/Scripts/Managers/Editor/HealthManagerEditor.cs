using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealthManager))]
public class HealthManagerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        

        DrawDefaultInspector();

        if(GUILayout.Button("Take Damage"))
        {
            var myTarget = (HealthManager)target;
            myTarget.TakeDamage();
        }
    }
}
