using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : StateControllerData
{
    public Vector3 nextPosition;
}

[CreateAssetMenu(menuName = "PluggableAI/States/Patrol")]
public class PatrolState : State
{
    public PatrolAction action;

    public int nextWaypoint = -1;

    public override void OnStateEnter(IStateController controller)
    {
        FetchNearestPatrolPoint(controller.GameObject.transform.position);
    }

    public override void OnStateExit(IStateController controller)
    {

    }

    public override void OnStateUpdate(IStateController controller)
    {
        base.OnStateUpdate(controller);
        //if current pos equal to waypoint, go to next waypoint
        if(nextWaypoint != -1)
        {
            CubeCoordinates position = Hex.FromWorld(controller.GameObject.transform.position);
            CubeCoordinates currentGoal = Hex.FromWorld(action.waypoints[nextWaypoint]);
            if(Hex.Distance(position, currentGoal) < 1f)
            {
                nextWaypoint = (nextWaypoint + 1) % action.waypoints.Length;
                SendData(controller);
            }
        }
    }

    public override void OnRoundStart(IStateController controller)
    {
        SendData(controller);
    }

    public override void OnRoundEnd(IStateController controller)
    {

    }

    private void SendData(IStateController controller)
    {

        PatrolData data = new PatrolData();
        data.nextPosition = controller.GameObject.transform.position;
        if (nextWaypoint != -1 && nextWaypoint < action.waypoints.Length)
        {
            data.nextPosition = action.waypoints[nextWaypoint];
        }
        controller.Receive(data);
    }

    private void FetchNearestPatrolPoint(Vector3 position)
    {
        if (action.waypoints != null && action.waypoints.Length > 0)
        {
            int nearestWaypoint = -1;
            float nearestDistance = Mathf.Infinity;
            for (int i = 0; i < action.waypoints.Length; i++)
            {
                CubeCoordinates from = Hex.FromWorld(position);
                CubeCoordinates to = Hex.FromWorld(action.waypoints[i]);
                float distance = Hex.Distance(from, to);
                if (distance < nearestDistance && distance >= 1f)
                {
                    nearestDistance = distance;
                    nearestWaypoint = i;
                }
            }

            nextWaypoint = nearestWaypoint;
        }
    }
}
