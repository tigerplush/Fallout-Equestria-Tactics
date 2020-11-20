using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public Vector3[] waypoints;
    public override void Act(StateController controller)
    {

    }

    private void Patrol(StateController controller)
    {

    }

    public override void DrawSceneGUI(SerializedObject serializedObject)
    {
        SerializedProperty waypointProperty = serializedObject.FindProperty("waypoints");

        for (int i = 0; i < waypointProperty.arraySize; i++)
        {
            SerializedProperty waypoint = waypointProperty.GetArrayElementAtIndex(i);

            waypoint.vector3Value = Handles.PositionHandle(waypoint.vector3Value, Quaternion.identity);

            if (i > 0)
            {
                Handles.DrawDottedLine(waypoints[i - 1], waypoints[i], .2f);
            }

            if (waypoints.Length > 1)
            {
                Handles.DrawDottedLine(waypoints[waypoints.Length - 1], waypoints[0], .2f);
            }
        }
    }
}
