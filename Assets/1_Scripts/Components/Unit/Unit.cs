using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
using DG.Tweening;

public partial class Unit : Entity
{
    public UnitObject unitObject;
    public UnitData unitData;

    public Vector3 position { get { return unitObject.cachedTransform.position; } }

    protected UnitManager unitManager;

    public bool IsUpdate;
    public bool IsMoving;
    public bool IsDeath;

    public Vector3 movePos = Vector3.zero;
    public Quaternion rotQuat = Quaternion.identity;

    protected StateMachine stateMachine { get; set; }

    static int key = 100000;
    public Unit() : base(key.ToString())
    {
        ++key;
    }

    public virtual void Initialize()
    {
        IsUpdate = true;
        IsDeath = false;
    }

    public virtual void Release()
    {
        IsUpdate = false;
        IsMoving = false;
        IsDeath = true;
        unitObject.Release();
    }

    public void SetUnitTable(int tableID)
    {
        unitData = new UnitData(tableID);
        unitData.Init();
    }

    public void SetUnitManager(UnitManager unitManager)
    {
        this.unitManager = unitManager;
    }

    public virtual void OnUpdate(float delta)
    {
        if (GameManager.Instance.gameStep != GameStep.Playing)
            return;

        if (IsUpdate && GameManager.Instance.gameStep == GameStep.Playing)
            stateMachine.Update();
    }
    public void OnFixedUpdate(float delta)
    {
        if (GameManager.Instance.gameStep != GameStep.Playing)
            return;
        if (IsUpdate && GameManager.Instance.gameStep == GameStep.Playing)
            stateMachine.FixedUpdate();
    }

    public void OnLateUpdate(float delta)
    {
        if (GameManager.Instance.gameStep != GameStep.Playing)
            return;
        if (IsUpdate && GameManager.Instance.gameStep == GameStep.Playing)
            stateMachine.LateUpdate();
    }

    public void ChangeFSMState(StateMachine.State state)
    {
        //죽은이후 상태변화 허용x
        if (IsDeath)
            return;        
        stateMachine.ChangeFSMState(state);
    }

    public void ChangeFSMUndoState()
    {
        stateMachine.ChangeFSMUndoState();
    }

    public StateMachine.State GetFSMState()
    {
        if (stateMachine == null)
            return StateMachine.State.Spawn;
        return stateMachine.GetFSMState();
    }

    public void SetAnimationParam(string param)
    {        
        int hash = Animator.StringToHash(param);
        unitObject.animator.SetTrigger(hash);
    }

    public void SetAnimationParam(string param, bool _isValid)
    {        
        int hash = Animator.StringToHash(param);
        unitObject.animator.SetBool(hash, _isValid);
    }

    public void SetAnimationParam(string param, int _isValid)
    {        
        int hash = Animator.StringToHash(param);
        unitObject.animator.SetInteger(hash, _isValid);
    }

    public bool IsPlaingAnimation(string param)
    {
        if (unitObject.animator.GetCurrentAnimatorStateInfo(0).IsName(param))
        {
            return true;
        }
        return false;
    }

    public virtual void UpGrade() { }

    public void SetHandle(StateMachine.State state, IStateHandle handle)
    {
        stateMachine.Get(state).SetHandle(handle);
    }

    public virtual void RegistHandler() { }

}
