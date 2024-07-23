using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Multi_PhotonEvent : MonoBehaviourPun
{
    public List<Multi_PlayerControl> multi_PlayerControls;

    public void AddPlayer(Multi_PlayerControl player)
    {
        photonView.RPC("Event_AddPlayer", RpcTarget.AllViaServer, player);
    }

    [PunRPC]
    public void Event_AddPlayer(Multi_PlayerControl player)
    {
        multi_PlayerControls.Add(player);
    }
}
