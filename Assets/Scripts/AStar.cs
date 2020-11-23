using System;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private class Priority : IComparable<Priority>
    {
        public CubeCoordinates coordinates;
        public float priority;

        public Priority(CubeCoordinates coordinates, float priority)
        {
            this.coordinates = coordinates;
            this.priority = priority;
        }

        public int CompareTo(Priority other)
        {
            return this.priority.CompareTo(other.priority);
        }
    }

    public static CubeCoordinates[] FindWay(CubeCoordinates start, CubeCoordinates target)
    {
        List<Priority> frontier = new List<Priority>();
        frontier.Add(new Priority(start, 0f));
        Dictionary<CubeCoordinates, CubeCoordinates> cameFrom = new Dictionary<CubeCoordinates, CubeCoordinates>();
        Dictionary<CubeCoordinates, float> costSoFar = new Dictionary<CubeCoordinates, float>();
        cameFrom.Add(start, null);
        costSoFar.Add(start, 0f);

        bool targetCanBeReached = false;
        for (; frontier.Count > 0 ; )
        {
            frontier.Sort();
            Priority current = frontier[0];
            frontier.RemoveAt(0);

            if (current.coordinates == target)
            {
                targetCanBeReached = true;
                break;
            }

            CubeCoordinates[] neighbors = current.coordinates.Neighbors();
            foreach (CubeCoordinates next in neighbors)
            {
                float nextCost;
                if (!TileCost(next, out nextCost))
                {
                    continue;
                }
                float newCost = costSoFar[current.coordinates] + nextCost;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    float newPriority = newCost + Hex.Distance(target, next);
                    frontier.Add(new Priority(next, newPriority));
                    cameFrom[next] = current.coordinates;
                }
            }
        }

        List<CubeCoordinates> path = new List<CubeCoordinates>();
        if (targetCanBeReached)
        {
            CubeCoordinates last = target;
            for (; last != null;)
            {
                path.Add(last);
                last = cameFrom[last];
            }
            path.Reverse();
        }
        return path.ToArray();
    }

    public static bool TileCost(CubeCoordinates tile, out float cost)
    {
        // Take tile in world coordinates
        // plus vector up
        // raycast down
        // return walkingCost
        Vector3 tilePosition = Hex.ToWorld(tile) + Vector3.up;
        Ray ray = new Ray(tilePosition, Vector3.down);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            WalkableSurface walkableSurface = hitInfo.collider.GetComponent<WalkableSurface>();
            if(walkableSurface != null)
            {
                cost = walkableSurface.cost;
                return walkableSurface.walkable;
            }
        }
        cost = Mathf.Infinity;
        return false;
    }

    public static bool IsUsable(CubeCoordinates tile)
    {
        // Take tile in world coordinates
        // plus Vector up
        // raycast down
        // if raycast not walkable surface => false
        // if raycast walkable surface => return isWalkable
        return true;
    }
}
