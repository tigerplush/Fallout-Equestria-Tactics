using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolBehaviour : StateMachineBehaviour
{
    public PatrolAction action;

    public int nextWaypoint = -1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);


        FetchNearestPatrolPoint(animator.rootPosition);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void FetchNearestPatrolPoint(Vector3 position)
    {
        if(action.waypoints != null && action.waypoints.Length > 0)
        {
            int nearestWaypoint = -1;
            float nearestDistance = Mathf.Infinity;
            for(int i = 0; i < action.waypoints.Length; i++)
            {
                CubeCoordinates from = Hex.FromWorld(position);
                CubeCoordinates to = Hex.FromWorld(action.waypoints[i]);
                float distance = Hex.Distance(from, to);
                if(distance < nearestDistance && distance >= 1f)
                {
                    nearestDistance = distance;
                    nearestWaypoint = i;
                }
            }

            nextWaypoint = nearestWaypoint;
        }
    }
}
