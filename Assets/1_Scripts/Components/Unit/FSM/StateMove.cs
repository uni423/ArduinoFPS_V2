using UnityEngine;

public class StateMove : StateBase
{
    public StateMove(Unit parent) : base(parent) { state = StateMachine.State.Move; }
    public override void Enter()
    {
        handle.OnEnter();
    }

    public override void Update(float delta)
    {
        if (handle == null)
            return;
        handle.OnUpdate(delta);
    }

    public override void LateUpate(float latedDelta)
    {
        if (handle == null)
            return;
        handle.OnLateUpdate(latedDelta);
    }

    public override void Exit()
    {
        handle.OnExit();
    }
    public override void HandleInput() { }
    public override void FixedUpdate(float fixedDelta)
    {
    }
}
