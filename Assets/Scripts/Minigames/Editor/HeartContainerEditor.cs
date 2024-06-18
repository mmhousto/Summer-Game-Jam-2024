using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HeartContainer))]
public class HeartContainerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        

        DrawDefaultInspector();

        if(GUILayout.Button("Show Damaged"))
        {
            var myTarget = (HeartContainer)target;
            myTarget.Damaged();
        }
        if(GUILayout.Button("Show Healed"))
        {
            var myTarget = (HeartContainer)target;
            myTarget.Healed();
        }
    }
}
