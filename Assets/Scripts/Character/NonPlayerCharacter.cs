using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class NonPlayerCharacter : Character, IStateController
{
    public State startingState;
    public State currentState;

    public bool aiActive;
    // AI
    // If enemy doesn't see you, idle, patrol or move randomly
    // if enemy sees you and in range, attack
    // if enemy sees you and out of range, move into range
    // if enemy gets hit and survives another hit, continue
    // if enemy gets hit and wouldn't survive, heal if possible
    // if enemy hasn't enough AP to do anything, end round

    protected override void Start()
    {
        base.Start();

        Transition(startingState);
    }

    public override void StartRound()
    {
        base.StartRound();

        if(currentState != null)
        {
            currentState.OnRoundStart(this);
        }
    }

    protected override void Update()
    {
        base.Update();

        if(aiActive && currentState != null)
        {
            currentState.OnStateUpdate(this);
        }

        if(hasTurn && ActionPoints == 0)
        {
            BattleManager.instance.NextRound();
        }
    }

    public void Receive(StateControllerData data)
    {
        PatrolData patrolData = data as PatrolData;
        if (patrolData != null)
        {
            CubeCoordinates target = Hex.FromWorld(patrolData.nextPosition);
            if (BattleManager.instance.IsEmpty(target))
            {
                CubeCoordinates[] path = AStar.FindWay(CubeCoordinates, target);
                SetPath(path);
            }
        }
    }

    public void Transition(State newState)
    {
        if(currentState != null)
        {
            currentState.OnStateExit(this);
        }
        currentState = newState;
        if(currentState != null)
        {
            currentState.OnStateEnter(this);
        }
    }

    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }
    }
}
