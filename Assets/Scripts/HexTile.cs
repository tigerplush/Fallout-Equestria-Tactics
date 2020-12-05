using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexTile
{
    public CubeCoordinates position;
    public int height;
    public int travelcost;

    public HexTile(CubeCoordinates position, int height)
    {
        this.position = position;
        this.height = height;
    }

    /// <summary>
    /// Creates vertices and triangles for this tile and adds them to a list
    /// </summary>
    /// <param name="vertices"></param>
    /// <param name="triangles"></param>
    public void CreateMesh(float tileSize, float tileHeight, ref List<Vector3> vertices, ref List<int> triangles)
    {
        for(int i = 0; i < height; i++)
        {
            CreateMesh(tileSize, tileHeight, ref vertices, ref triangles, i);
        }
    }

    public void CreateMesh(float tileSize, float tileHeight, ref List<Vector3> vertices, ref List<int> triangles, int currentHeight)
    {
        int vertexOffset = vertices.Count;
        for (int i = 0; i < 2; i++)
        {
            //Create bottomside
            for (int j = 0; j < 6; j++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * j * 60f) * tileSize;
                float z = Mathf.Cos(Mathf.Deg2Rad * j * 60f) * tileSize;
                Vector3 point = new Vector3(x, (currentHeight + i) * tileHeight, z);
                point += Hex.ToWorld(position) * tileSize;
                vertices.Add(point);
            }
        }

        //connect vertices
        //first the bottom face
        triangles.Add(vertexOffset + 0);
        triangles.Add(vertexOffset + 5);
        triangles.Add(vertexOffset + 1);

        triangles.Add(vertexOffset + 1);
        triangles.Add(vertexOffset + 5);
        triangles.Add(vertexOffset + 2);

        triangles.Add(vertexOffset + 2);
        triangles.Add(vertexOffset + 5);
        triangles.Add(vertexOffset + 4);

        triangles.Add(vertexOffset + 2);
        triangles.Add(vertexOffset + 4);
        triangles.Add(vertexOffset + 3);

        //upper face
        triangles.Add(vertexOffset + 6);
        triangles.Add(vertexOffset + 7);
        triangles.Add(vertexOffset + 11);

        triangles.Add(vertexOffset + 7);
        triangles.Add(vertexOffset + 8);
        triangles.Add(vertexOffset + 11);

        triangles.Add(vertexOffset + 8);
        triangles.Add(vertexOffset + 10);
        triangles.Add(vertexOffset + 11);

        triangles.Add(vertexOffset + 8);
        triangles.Add(vertexOffset + 9);
        triangles.Add(vertexOffset + 10);

        //sides
        triangles.Add(vertexOffset + 0);
        triangles.Add(vertexOffset + 7);
        triangles.Add(vertexOffset + 6);

        triangles.Add(vertexOffset + 0);
        triangles.Add(vertexOffset + 1);
        triangles.Add(vertexOffset + 7);

        triangles.Add(vertexOffset + 1);
        triangles.Add(vertexOffset + 8);
        triangles.Add(vertexOffset + 7);

        triangles.Add(vertexOffset + 1);
        triangles.Add(vertexOffset + 2);
        triangles.Add(vertexOffset + 8);

        triangles.Add(vertexOffset + 2);
        triangles.Add(vertexOffset + 9);
        triangles.Add(vertexOffset + 8);

        triangles.Add(vertexOffset + 2);
        triangles.Add(vertexOffset + 3);
        triangles.Add(vertexOffset + 9);

        triangles.Add(vertexOffset + 3);
        triangles.Add(vertexOffset + 10);
        triangles.Add(vertexOffset + 9);

        triangles.Add(vertexOffset + 3);
        triangles.Add(vertexOffset + 4);
        triangles.Add(vertexOffset + 10);

        triangles.Add(vertexOffset + 4);
        triangles.Add(vertexOffset + 11);
        triangles.Add(vertexOffset + 10);

        triangles.Add(vertexOffset + 4);
        triangles.Add(vertexOffset + 5);
        triangles.Add(vertexOffset + 11);

        triangles.Add(vertexOffset + 5);
        triangles.Add(vertexOffset + 0);
        triangles.Add(vertexOffset + 6);

        triangles.Add(vertexOffset + 5);
        triangles.Add(vertexOffset + 6);
        triangles.Add(vertexOffset + 11);
    }
}
