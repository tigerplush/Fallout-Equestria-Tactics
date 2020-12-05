using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTilemap : MonoBehaviour
{
    public MeshCollider meshCollider;
    public MeshFilter meshFilter;
    public float tileSize = 1f;
    public float tileHeight = 1f;

    public List<HexTile> tiles = new List<HexTile>();

    private void Start()
    {
        CreateMesh();
    }

    public void CreateMesh()
    {
        if(!float.IsNaN(tileSize))
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            foreach (HexTile tile in tiles)
            {
                tile.CreateMesh(tileSize, tileHeight, ref vertices, ref triangles);
            }

            Mesh mesh = new Mesh();
            mesh.name = "One Hex";
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            meshCollider.sharedMesh = mesh;

            meshFilter.mesh = mesh;
        }
    }

    public void Add(CubeCoordinates coordinates)
    {
        if(!tiles.Exists(tile => tile.position == coordinates))
        {
            tiles.Add(new HexTile(coordinates, 1));
            CreateMesh();
        }
    }
}
