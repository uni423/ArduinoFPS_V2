using UnityEngine;

public class HelperMoveHandle : StateHandle
{
    //HelperUnit helper;
    /// <summary>
    /// 
    /// </summary>
    public override void OnEnter()
    {
        //helper = parent as HelperUnit;
        //parent.SetAnimationParam("IsNormal", true);
        //parent.SetAnimationParam("IsFarming", false);
        //parent.unitObject.nevMeshAgent.SetDestination(helper.movePos);
        //parent.unitObject.nevMeshAgent.isStopped = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnUpdate(float delta)
    {
        //if (parent.unitObject.animator != null)
        //{
        //    if (parent.IsEmpty())
        //    {
        //        parent.SetAnimationParam("IsNormal", true);
        //        parent.SetAnimationParam("IsCarry", false);
        //    }
        //    else
        //    {
        //        parent.SetAnimationParam("IsNormal", false);
        //        parent.SetAnimationParam("IsCarry", true);
        //    }
        //}

        //if (Vector3.Distance(parent.position, helper.movePos) < 0.5f)
        //{
        //    WorkState workState = parent.GetWorkState();
        //    if (workState == WorkState._WorkMoving)
        //    {
        //        if (EnvironmentManager.Instance.curruntTimeType == TimeType.SleepTime)
        //            parent.ChangeFSMState(StateMachine.State.Idle);
        //        else if (EnvironmentManager.Instance.curruntTimeType == TimeType.MealTime ||
        //        parent.workerType == WorkerType.NoneWorker)
        //            parent.ChangeFSMState(StateMachine.State.Patrol);
        //        else
        //            parent.ChangeFSMState(StateMachine.State.Idle);
        //    }
        //    else if (workState == WorkState._Moving)
        //    {
        //        if (parent.IsAb_OXY) //산소 충전소로 가야하는 상태
        //        {
        //            if ((MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.OxygenGenerator][0].building as OxygenGenerator).waitingUnitList.Count > 0
        //            && (MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.OxygenGenerator][0].building as OxygenGenerator).waitingUnitList[0] == helper)
        //            {
        //                if ((MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.OxygenGenerator][0].building as OxygenGenerator).StartChargeOxyByFirstUnit() == false)
        //                    parent.ChangeFSMState(StateMachine.State.Idle);
        //            }
        //            else parent.ChangeFSMState(StateMachine.State.Idle);
        //        }
        //        else if (parent.IsAb_DESEASE)
        //        {
        //            if ((MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Hospital][0].building as Hospital).waitingUnitList.Count > 0
        //            && (MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Hospital][0].building as Hospital).waitingUnitList[0] == helper)
        //            {
        //                if ((MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Hospital][0].building as Hospital).StartCureByFirstUnit() == false)
        //                    parent.ChangeFSMState(StateMachine.State.Idle);
        //            }
        //            else parent.ChangeFSMState(StateMachine.State.Idle);
        //        }
        //        else if (EnvironmentManager.Instance.curruntTimeType == TimeType.MealTime)
        //        {
        //            if (parent.IsFoodCarrying)
        //            {
        //                //배식 완료, 자리로 이동
        //                (MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Kitchen][0].building as Kitchen).emptyDistributeList.Add(helper.distributeTarget);
        //                helper.distributeTarget = null;
        //                (MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Kitchen][0].building as Kitchen).StartDistributeFoodByFirstUnit();
        //                helper.movePos = helper.tableTarget.position;
        //                parent.ChangeFSMState(StateMachine.State.Eat);
        //            }
        //            else
        //            {
        //                if ((MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Kitchen][0].building as Kitchen).waitingUnitList.Count > 0
        //                    && (MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Kitchen][0].building as Kitchen).waitingUnitList[0] == helper)
        //                {
        //                    if ((MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Kitchen][0].building as Kitchen).StartDistributeFoodByFirstUnit() == false)
        //                        parent.ChangeFSMState(StateMachine.State.Idle);
        //                }
        //                else parent.ChangeFSMState(StateMachine.State.Idle);
        //                //(MapManager.Instance.curArea.buildingManager.loaderDictionary[BuildingType.Kitchen][0].building as Kitchen).StartDistributeFoodByUnit();
        //            }
        //        }
        //        else if (EnvironmentManager.Instance.curruntTimeType == TimeType.SleepTime)
        //            parent.ChangeFSMState(StateMachine.State.Sleep);
        //        else if (EnvironmentManager.Instance.curruntTimeType == TimeType.WorkingTime)
        //            parent.ChangeFSMState(StateMachine.State.Patrol);
        //        return;
        //    }
        //    else if (workState == WorkState._Taking || workState == WorkState._Giving)
        //    {
        //        parent.ChangeFSMState(StateMachine.State.Idle);
        //        return;
        //    }
        //}

        //parent.unitObject.nevMeshAgent.SetDestination(helper.movePos);
        ////Vector3 lookrotation = (helper.movePos - parent.position).normalized;
        ////parent.unitObject.cachedTransform.rotation = Quaternion.Slerp(parent.unitObject.cachedTransform.rotation, Quaternion.LookRotation(lookrotation), 10f * Time.deltaTime);        
        //parent.unitObject.animator.SetFloat("SpeedSide", (parent.unitObject.cachedTransform.forward * parent.unitObject.nevMeshAgent.velocity.z).magnitude);
        //parent.unitObject.animator.SetFloat("SpeedForward", (parent.unitObject.cachedTransform.right * parent.unitObject.nevMeshAgent.velocity.x).magnitude);

    }

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