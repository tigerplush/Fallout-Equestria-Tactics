using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexTilemap))]
public class HexTilemapEditor : Editor
{

    public void OnSceneGUI()
    {
        HexTilemap tilemap = target as HexTilemap;

        //if there is no hextile, draw a single button
        if (tilemap.tiles.Count > 0)
        {
            EditorGUI.BeginChangeCheck();
            List<CubeCoordinates> neighbors = new List<CubeCoordinates>();
            //for every hextile, draw buttons to their neighbors not in hextiles
            foreach(HexTile tile in tilemap.tiles)
            {
                int height = tile.height;
                height = (int)Handles.ScaleSlider(height, Hex.ToWorld(tile.position) * tilemap.tileSize + Vector3.up * height * tilemap.tileHeight, Vector3.up, Quaternion.identity, 1f, 1f);
                tile.height = Mathf.Max(1, height);

                CubeCoordinates[] currentNeighbors = tile.position.Neighbors();
                foreach(CubeCoordinates neighbor in currentNeighbors)
                {
                    if(!tilemap.tiles.Exists(currentTile => currentTile.position == neighbor))
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            foreach(CubeCoordinates neighbor in neighbors)
            {
                if (Handles.Button(Hex.ToWorld(neighbor) * tilemap.tileSize, Quaternion.identity, .1f, .1f, Handles.SphereHandleCap))
                {
                    tilemap.Add(neighbor);
                }
            }
            if(EditorGUI.EndChangeCheck())
            {
                tilemap.CreateMesh();
            }
        }
        else
        {
            if(Handles.Button(tilemap.transform.position, Quaternion.identity, .1f, .1f, Handles.SphereHandleCap))
            {
                tilemap.Add(new CubeCoordinates());
            }
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if(EditorGUI.EndChangeCheck())
        {
            HexTilemap tilemap = target as HexTilemap;
            tilemap.CreateMesh();
        }
    }
}
