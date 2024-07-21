using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonEvent : MonoBehaviourPun
{
    public void SceneLoadEvent(string SceneName)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Event_SceneLoad", RpcTarget.AllViaServer, SceneName);
        }
    }

    [PunRPC]
    public void Event_SceneLoad(string SceneName)
    {
        PhotonNetwork.LoadLevel(SceneName);
    }
}
