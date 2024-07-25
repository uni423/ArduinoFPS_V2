using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

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

    public void Hit(AttackType type)
    {
        photonView.RPC("Event_Hit", RpcTarget.AllViaServer, (int)type, PhotonNetwork.LocalPlayer.GetPlayerNumber());
    }

    [PunRPC]
    public void Event_Hit(int type, int playerNumber)
    {
        if (rabbit != null)
        {
            rabbit.lastDamagedPlayerNumber = playerNumber;
            rabbit.Hit((AttackType)type);
        }
    }
}
