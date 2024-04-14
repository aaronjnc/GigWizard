using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(Tilemap))]
public class MapCollisionGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Tilemap map = (Tilemap)target;
        GameObject cubeCollider = null;
        cubeCollider = (GameObject)EditorGUILayout.ObjectField ("Cube Collider", cubeCollider, typeof(GameObject), false);
        if(GUILayout.Button("Generate Colliders"))
        { 
            
        }   
    }
}