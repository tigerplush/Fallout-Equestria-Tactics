using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public Transition[] transitions;

    public abstract void OnStateEnter(IStateController controller);
    public abstract void OnStateExit(IStateController controller);
    public virtual void OnStateUpdate(IStateController controller)
    {
        foreach(Transition transition in transitions)
        {
            if(transition.condition)
            {
                controller.Transition(transition.nextState);
            }
        }
    }

    public abstract void OnRoundStart(IStateController controller);
    public abstract void OnRoundEnd(IStateController controller);
}
