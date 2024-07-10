using UnityEngine;

public class RabbitDeSpawnHandle : StateHandle
{
    /// <summary>
    /// 
    /// </summary>
    public override void OnEnter()
    {
        InGameManager.ObjectPooling.Despawn(parent.unitObject.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delta"></param>
    public override void OnUpdate(float delta)
    {
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
