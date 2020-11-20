using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : Character, StateController
{
    public State currentState;
    public bool aiActive;
    // AI
    // If enemy doesn't see you, idle, patrol or move randomly
    // if enemy sees you and in range, attack
    // if enemy sees you and out of range, move into range
    // if enemy gets hit and survives another hit, continue
    // if enemy gets hit and wouldn't survive, heal if possible
    // if enemy hasn't enough AP to do anything, end round

    protected override void Update()
    {
        base.Update();

        if(aiActive)
        {
            currentState.UpdateState(this);
        }
    }
}
