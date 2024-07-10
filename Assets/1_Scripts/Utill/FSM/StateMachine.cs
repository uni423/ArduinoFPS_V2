using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StateMachine
{
    public enum State
    {
        Spawn,
        Move,
        Hit, 
        Death,
        DeSpawn, 
    }

    private StateBase oldState;
    private StateBase currentState;

    private Dictionary<State, StateBase> dicToState;

    public StateMachine()
    {
        currentState = null;
    }

    public void Initialize()
    {
        dicToState = new Dictionary<State, StateBase>();
    }

    public void Regist(State id, StateBase state)
    {
        dicToState.Add(id, state);
    }

    public StateBase Get(State id)
    {
        if (dicToState.ContainsKey(id))
            return dicToState[id];

        return null;
    }

    public void ChangeFSMState(State state)
    {
        if (dicToState.ContainsKey(state))
        {

            if (currentState != null)
            {
                oldState = currentState;
                currentState.Exit();
            }

            currentState = dicToState[state];
            currentState.Enter();

        }
    }

    public void ChangeFSMUndoState()
    {
        if (currentState != null)
            currentState.Exit();

        currentState = oldState;
        currentState.Enter();

        oldState = currentState;
    }

    public State GetFSMState()
    {
        if (currentState == null)
            return State.Spawn;
        return currentState.state;
    }


    public void Update()
    {
        if (dicToState.Count == 0)
            return;

        if (currentState == null)
        {
            currentState = dicToState[State.Spawn];
            currentState.Enter();
            return;
        }

        if (currentState != null)
        {
            currentState.Update(Time.deltaTime);

            currentState.HandleInput();
        }
    }

    public void FixedUpdate()
    {
        if (currentState != null) { currentState.FixedUpdate(Time.fixedDeltaTime); }
    }

    public void LateUpdate()
    {
        if (currentState != null) { currentState.LateUpate(Time.deltaTime); }
    }
}