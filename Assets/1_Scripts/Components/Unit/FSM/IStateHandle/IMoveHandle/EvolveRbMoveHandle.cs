using UnityEngine;

public class EvolveRbMoveHandle : StateHandle
{
    Transform cachedTransofrm;
    int speed;

    float curTime = 0f;
    float maxTime = 10f;


    /// <summary>
    /// 
    /// </summary>
    public override void OnEnter()
    {
        cachedTransofrm = parent.unitObject.cachedTransform;
        speed = parent.unitData.move;

        parent.SetAnimationParam("IsMove", true);
        parent.SetAnimationParam("MoveType", 3);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delta"></param>
    public override void OnUpdate(float delta)
    {
        cachedTransofrm.position += cachedTransofrm.forward * delta * speed;

        curTime += delta;
        if (curTime >= maxTime)
            parent.ChangeFSMState(StateMachine.State.DeSpawn);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lateDelta"></param>
    public override void OnLateUpdate(float lateDelta)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnExit()
    {
    }
}
