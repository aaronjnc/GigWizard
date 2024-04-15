using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

[CustomEditor(typeof(Tilemap))]
public class MapCollisionGenerator : Editor
{
    private const string parentObjName = "World Colliders";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Tilemap map = (Tilemap)target;
        if(GUILayout.Button("Generate Colliders"))
        { 
            GameObject prevParent = GameObject.Find(parentObjName);
            if (prevParent != null)
            {
                DestroyImmediate(prevParent);
            }
            GameObject parentObj = new GameObject(parentObjName);
            for (int x = map.cellBounds.min.x; x < map.cellBounds.max.x; x++)
            {
                for (int y = map.cellBounds.min.y; y < map.cellBounds.max.y; y++)
                {
                    for (int z =  map.cellBounds.min.z; z < map.cellBounds.max.z; z++)
                    {
                        if (map.HasTile(new Vector3Int(x, y, z)))
                        {
                            bool bSurrounded = true;
                            for (int xMod = -1; xMod <= 1; xMod++)
                            {
                                for (int yMod = -1; yMod <= 1; yMod++)
                                {
                                    if (!map.HasTile(new Vector3Int(x + xMod, y + yMod, z)))
                                    {
                                        bSurrounded = false;
                                        break;
                                    }
                                }
                                if (!bSurrounded)
                                {
                                    break;
                                }
                            }
                            if (bSurrounded)
                            {
                                continue;
                            }
                            Vector3 spawnLoc = map.CellToWorld(new Vector3Int(x, y, z));
                            spawnLoc.y = 1;
                            spawnLoc += new Vector3(.5f, 0, .5f);
                            GameObject newCube = new GameObject(x + " " + y + " " + z);
                            newCube.transform.position = spawnLoc;
                            newCube.AddComponent<BoxCollider>();
                            NavMeshObstacle obstacle = newCube.AddComponent<NavMeshObstacle>();
                            obstacle.carving = true;
                            newCube.transform.SetParent(parentObj.transform);
                        }
                    }
                }
            }
        }   
    }
}