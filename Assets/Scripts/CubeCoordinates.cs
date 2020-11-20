using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains data and algorhitms for hexagonal cube coordinates
/// </summary>
public class CubeCoordinates : IEquatable<CubeCoordinates>
{
    public static CubeCoordinates UpRight   = new CubeCoordinates(1f, 0f, -1f);
    public static CubeCoordinates Right     = new CubeCoordinates(1f, -1f, 0f);
    public static CubeCoordinates DownRight = new CubeCoordinates(0f, -1f, 1f);
    public static CubeCoordinates DownLeft  = new CubeCoordinates(-1f, 0f, 1f);
    public static CubeCoordinates Left      = new CubeCoordinates(-1f, 1f, 0f);
    public static CubeCoordinates UpLeft    = new CubeCoordinates(0f, 1f, -1f);

    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    /// <summary>
    /// Default constructor for CubeCoordinates
    /// </summary>
    /// <param name="x">sets x coordinate, defaults to 0</param>
    /// <param name="y">sets y coordinate, defaults to 0</param>
    /// <param name="z">sets z coordinate, defaults to 0</param>
    public CubeCoordinates(float x = 0f, float y = 0f, float z = 0f)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public CubeCoordinates[] Neighbors()
    {
        List<CubeCoordinates> neighbors = new List<CubeCoordinates>();

        neighbors.Add(this + UpRight);
        neighbors.Add(this + Right);
        neighbors.Add(this + DownRight);
        neighbors.Add(this + DownLeft);
        neighbors.Add(this + Left);
        neighbors.Add(this + UpLeft);

        return neighbors.ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Cube coordinates as string</returns>
    public override string ToString()
    {
        return $"({x}, {y}, {z})";
    }

    public static CubeCoordinates operator+(CubeCoordinates left, CubeCoordinates right)
    {
        CubeCoordinates coordinates = new CubeCoordinates();
        coordinates.x = left.x + right.x;
        coordinates.y = left.y + right.y;
        coordinates.z = left.z + right.z;
        return coordinates;
    }

    public static CubeCoordinates operator-(CubeCoordinates left, CubeCoordinates right)
    {
        CubeCoordinates coordinates = new CubeCoordinates();
        coordinates.x = left.x - right.x;
        coordinates.y = left.y - right.y;
        coordinates.z = left.z - right.z;
        return coordinates;
    }

    public static bool operator==(CubeCoordinates left, CubeCoordinates right)
    {
        if(ReferenceEquals(left, right))
        {
            return true;
        }

        if (ReferenceEquals(null, left))
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(CubeCoordinates left, CubeCoordinates right)
    {
        return !(left == right);
    }

    public bool Equals(CubeCoordinates other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return this.x == other.x && this.y == other.y && this.z == other.z;
    }

    public override bool Equals(object obj)
    {
        if(ReferenceEquals(null, obj))
        {
            return false;
        }
        if(ReferenceEquals(this, obj))
        {
            return true;
        }
        if(obj.GetType() != this.GetType())
        {
            return false;
        }
        return Equals(obj as CubeCoordinates);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            const int HashingBase = (int)2166136261;
            const int HashingMultiplier = 16777619;

            int hash = HashingBase;
            hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, x) ? x.GetHashCode() : 0);
            hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, y) ? y.GetHashCode() : 0);
            hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, z) ? z.GetHashCode() : 0);
            return hash;
        }
    }
}
