using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPointer : MonoBehaviour
{
    public static HexPointer instance = null;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public LayerMask pointableLayers;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateMesh();
    }

    private void Update()
    {
        if(meshRenderer.enabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, pointableLayers))
            {
                CubeCoordinates tileCoordinates = Hex.FromWorld(hitInfo.point);
                transform.position = Hex.ToWorld(tileCoordinates);
            }
        }
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.name = "Single Hex";


        Vector3[] vertices = new Vector3[6];
        for(int i = 0; i < 6; i++)
        {
            float x = Mathf.Sin(i * 60f / 360f * Mathf.PI * 2f);
            float z = Mathf.Cos(i * 60f / 360f * Mathf.PI * 2f);
            vertices[i] = new Vector3(x, 0f, z);
        }

        int[] triangles = new int[12];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 5;

        triangles[3] = 1;
        triangles[4] = 2;
        triangles[5] = 5;

        triangles[6] = 2;
        triangles[7] = 3;
        triangles[8] = 4;

        triangles[9] = 2;
        triangles[10] = 4;
        triangles[11] = 5;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    public void Disable()
    {
        meshRenderer.enabled = false;
    }

    public void Enable()
    {
        meshRenderer.enabled = true;
    }
}
