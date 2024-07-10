using UnityEngine;

public class StrongRbMoveHandle : StateHandle
{
    Transform cachedTransofrm;
    Rigidbody cachedRigidbody;
    int speed;
    int JumpPower;

    float curTime = 0f;
    float maxTime = 10f;

    float curJumpTime = 0f;
    float maxJumpTime = 1f;


    /// <summary>
    /// 
    /// </summary>
    public override void OnEnter()
    {
        cachedTransofrm = parent.unitObject.cachedTransform;
        cachedRigidbody = parent.unitObject.cachedRigidbody;
        speed = parent.unitData.move;
        JumpPower = (parent.unitObject as StrongRbUnitObject).JumpPower;

        parent.SetAnimationParam("IsMove", true);
        parent.SetAnimationParam("MoveType", 4);
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

        curJumpTime += delta;
        if (curJumpTime >= maxJumpTime)
        {
            curJumpTime -= maxJumpTime;
            cachedRigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
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
