using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains algorhitms for handling hexagonal tiles
/// </summary>
public class Hex
{
    /// <summary>
    /// Contains data and algorhitms for hexagonal axial coordinates
    /// </summary>
    public class AxialCoordinates
    {
        public float q = 0f;
        public float r = 0f;

        public AxialCoordinates(float q = 0f, float r = 0f)
        {
            this.q = q;
            this.r = r;
        }

        public override string ToString()
        {
            return $"({q}, {r})";
        }
    }

    public static CubeCoordinates FromWorld(Vector3 position)
    {
        float size = 1f;
        float q = (Mathf.Sqrt(3) / 3f * position.x - 1f / 3f * position.z) / size;
        float r = (                                  2f / 3f * position.z) / size;
        CubeCoordinates cubeCoordinates;
        cubeCoordinates = RoundCubeCoordinates(AxialToCubeCoordinates(q, r));
        return cubeCoordinates;
    }

    public static Vector3 ToWorld(CubeCoordinates cubeCoordinates)
    {
        AxialCoordinates axialCoordinates = CubeToAxialCoordinates(cubeCoordinates);

        float size = 1f;
        float x = size * (Mathf.Sqrt(3) * axialCoordinates.q + Mathf.Sqrt(3) / 2f * axialCoordinates.r);
        float z = size * (                                                3f / 2f * axialCoordinates.r);
        return new Vector3(x, 0f, z);
    }

    public static CubeCoordinates RoundCubeCoordinates(CubeCoordinates cubeCoordinates)
    {
        return RoundCubeCoordinates(cubeCoordinates.x, cubeCoordinates.y, cubeCoordinates.z);
    }

    public static CubeCoordinates RoundCubeCoordinates(float x, float y, float z)
    {
        float rx = Mathf.Round(x);
        float ry = Mathf.Round(y);
        float rz = Mathf.Round(z);

        float xDifference = Mathf.Abs((float)rx - x);
        float yDifference = Mathf.Abs((float)ry - y);
        float zDifference = Mathf.Abs((float)rz - z);

        if (
            xDifference > yDifference &&
            xDifference > zDifference
            )
        {
            rx = -ry - rz;
        }
        else if(yDifference > zDifference)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }

        return new CubeCoordinates(rx, ry, rz);
    }

    public static AxialCoordinates CubeToAxialCoordinates(CubeCoordinates cubeCoordinates)
    {
        return CubeToAxialCoordinates(cubeCoordinates.x, cubeCoordinates.y, cubeCoordinates.z);
    }

    public static AxialCoordinates CubeToAxialCoordinates(float x, float y, float z)
    {
        float q = x;
        float r = z;
        return new AxialCoordinates(q, r);
    }

    public static CubeCoordinates AxialToCubeCoordinates(AxialCoordinates axialCoordinates)
    {
        return AxialToCubeCoordinates(axialCoordinates.q, axialCoordinates.r);
    }

    public static CubeCoordinates AxialToCubeCoordinates(float q, float r)
    {
        float x = q;
        float z = r;
        float y = -x - z;
        return new CubeCoordinates(x, y, z);
    }

    public static float Distance(CubeCoordinates from, CubeCoordinates to)
    {
        return (Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y) + Mathf.Abs(from.z - to.z)) / 2f;
    }
}
