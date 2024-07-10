using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class UnitObject : MonoBehaviour
{    
    public Unit unit { get; protected set; }
    public Animator animator;
    public GameObject model;
    public SkinnedMeshRenderer mesh { set; get; }
    public StateMachine.State curState;

    public Material cachedMaterial { set; get; }
    /// <summary>
    /// get/private set
    /// </summary>
    public Transform cachedTransform { get; private set; }
    public CapsuleCollider bodyCol { get; private set; }
    public Rigidbody cachedRigidbody { get; private set; }

    private void Awake()
    {
        cachedTransform = this.transform;
        bodyCol = this.GetComponent<CapsuleCollider>();
        cachedRigidbody = this.GetComponent<Rigidbody>();
        //cachedRigidbody.useGravity = false;
    }

    private void OnDisable()
    {
        if (unit != null)
            unit.IsUpdate = false;
    }

    public virtual void Release()
    {
    }

    public virtual void SetAgent(Unit _unit)
    {
        this.unit = _unit;
    }

    public virtual void LoadModel(string path)
    {
        if (model != null)
            Destroy(model);
        model = GameObject.Instantiate(Resources.Load($"Model/{path}") as GameObject);
        model.transform.SetParent(cachedTransform);
        model.transform.localPosition = Vector3.zero;
        animator = model.GetComponent<Animator>();
        mesh = model.GetComponentInChildren<SkinnedMeshRenderer>();
        //cachedMaterial = new Material(mesh.material.shader);
        //cachedMaterial.CopyPropertiesFromMaterial(mesh.material);
        //mesh.material = cachedMaterial;
    }

    public virtual void SetController()
    {
    }

    public void Update()
    {        
        curState = unit.GetFSMState();
    }
}
