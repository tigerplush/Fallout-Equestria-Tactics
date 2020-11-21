using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateControllerData
{

}

public interface IStateController
{
    void Receive(StateControllerData data);
    void Transition(State newState);
    GameObject GameObject
    {
        get;
    }
}
