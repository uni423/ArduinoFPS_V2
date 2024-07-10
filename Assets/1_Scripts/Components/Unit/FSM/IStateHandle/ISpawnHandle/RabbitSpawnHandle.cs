using UnityEngine;

public class RabbitSpawnHandle : StateHandle
{
    RabbitUnitObject unitObject;
    /// <summary>
    /// 
    /// </summary>
    public override void OnEnter()
    {
        unitObject = parent.unitObject as RabbitUnitObject;
        //Sound
        unitObject.audioSource.clip = unitObject.spawnSFX[Random.Range(0, unitObject.spawnSFX.Length)];
        unitObject.audioSource.Play();
        //Effect
        InGameManager.ObjectPooling.Spawn("Rabbit_Spawn", null, unitObject.cachedTransform.position);
    }
    float time = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delta"></param>
    public override void OnUpdate(float delta)
    {
        time += delta;
        if (time >= 1f)
            parent.ChangeFSMState(StateMachine.State.Move);
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
