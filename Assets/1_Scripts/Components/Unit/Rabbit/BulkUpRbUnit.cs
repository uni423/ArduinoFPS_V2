using UnityEngine;

public class BulkUpRbUnit : RabbitUnit
{
    BulkUpRbUnitObject unitObject;

    public override void Initialize()
    {
        IsUpdate = true;
        IsDeath = false;

        if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
            unitObject = Multi_InGameManager.PHObjectPooling.PoolInstantiate("Unit/" + this.unitData.model, Vector3.zero, Quaternion.identity).GetComponent<BulkUpRbUnitObject>();
        else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
            unitObject = InGameManager.ObjectPooling.Spawn<BulkUpRbUnitObject>(this.unitData.model);
        //unitObject.model.transform.localPosition = Vector3.zero;
        //unitObject.model.transform.localRotation = Quaternion.identity;
        base.unitObject = unitObject;
        unitObject.LoadModel("");

        unitObject.SetAgent(this);
        unitObject.SetController();

        stateMachine = new StateMachine();
        stateMachine.Initialize();
        stateMachine.Regist(StateMachine.State.Spawn, new StateSpawn(this));
        stateMachine.Regist(StateMachine.State.Move, new StateMove(this));
        stateMachine.Regist(StateMachine.State.Hit, new StateHit(this));
        stateMachine.Regist(StateMachine.State.Death, new StateDeath(this));
        stateMachine.Regist(StateMachine.State.DeSpawn, new StateDeSpawn(this));

        SetStat();
        RegistHandler();
    }

    public override void Hit(AttackType type)
    {
        if (IsDeath == true)
            return;

        switch (type)
        {
            case AttackType.Normal:

                //Sound
                //Effect

                if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
                    InGameManager.ObjectPooling.Spawn("Rabbit_Hit", null, unitObject.cachedTransform.position);
                else if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
                    Multi_InGameManager.PHObjectPooling.PoolInstantiate("Effect/Rabbit_Hit", unitObject.cachedTransform.position, Quaternion.identity);

                hp -= 10;
                //ChangeFSMState(StateMachine.State.Hit);
                CheckDead();
                break;
        }
    }
}
