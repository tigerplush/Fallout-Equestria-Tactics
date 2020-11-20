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

        for (; frontier.Count > 0 ; )
        {
            frontier.Sort();
            Priority current = frontier[0];
            frontier.RemoveAt(0);

            if (current.coordinates == target)
            {
                break;
            }

            CubeCoordinates[] neighbors = current.coordinates.Neighbors();
            foreach (CubeCoordinates next in neighbors)
            {
                float newCost = costSoFar[current.coordinates] + 1f;
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
        CubeCoordinates last = target;
        for(; last != null; )
        {
            path.Add(last);
            last = cameFrom[last];
        }
        path.Reverse();
        return path.ToArray();
    }
}
