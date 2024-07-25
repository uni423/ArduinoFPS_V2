using UnityEngine;

public class EvolveRbDeathHandle : StateHandle
{
    RabbitUnit unit;
    float time = 0;
    /// <summary>
    /// 
    /// </summary>
    public override void OnEnter()
    {
        unit = parent as RabbitUnit;
        unit.IsDeath = true;
        unit.SetAnimationParam("IsDeath", true);
        unit.unitObject.cachedRigidbody.useGravity = true;

        if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
        {
            Multi_InGameManager.Instance.photonEvent.AddScore(parent.unitData.point);
            Multi_InGameManager.Instance.photonEvent.SetCombo(unit.lastDamagedPlayerNumber);
        }
        else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
        {
            InGameManager.Instance.AddScore(parent.unitData.point);
            InGameManager.Instance.playerControl.SetCombo();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delta"></param>
    public override void OnUpdate(float delta)
    {
        time += delta;
        if (time >= 3f)
        {
            if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
                Multi_InGameManager.PHObjectPooling.PoolDestroy(parent.unitObject.gameObject);
            else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
                InGameManager.ObjectPooling.Despawn(parent.unitObject.gameObject);
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
