using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine
{
    public BossState currentState {get; private set;}

    public void Initialise(BossState startingState){
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(BossState newState){
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
