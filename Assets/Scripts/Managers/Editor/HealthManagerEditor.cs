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
        
        if(GUILayout.Button("Heal Damage"))
        {
            var myTarget = (HealthManager)target;
            myTarget.Heal();
        }
        
        
        if(GUILayout.Button("Add Container"))
        {
            var myTarget = (HealthManager)target;
            myTarget.AddHeartPooled();
        }
        
        if(GUILayout.Button("Remove Container"))
        {
            var myTarget = (HealthManager)target;
            myTarget.RemoveHeartContainer();
        }
        
        if(GUILayout.Button("Simulate Start Game"))
        {
            var myTarget = (HealthManager)target;
            myTarget.StartGame();
        }
        
        if(GUILayout.Button("Simulate End Game"))
        {
            var myTarget = (HealthManager)target;
            myTarget.EndGame();
        }
    }
}
