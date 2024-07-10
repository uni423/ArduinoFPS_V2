using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitUnitObject : UnitObject
{
    RabbitUnit rabbit;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] spawnSFX;

    public override void LoadModel(string path)
    {
        //animator = model.GetComponent<Animator>();
        //mesh = model.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public override void SetAgent(Unit _unit)
    {
        base.SetAgent(_unit);

        rabbit = unit as RabbitUnit;
    }
}
