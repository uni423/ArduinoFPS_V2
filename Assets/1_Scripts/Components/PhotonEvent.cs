using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Reflection;
using UnityEngine.Analytics;

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

    public void SetSelectStage(int index)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Event_SetSelectStage", RpcTarget.AllViaServer, index);
        }
    }

    [PunRPC]
    public void Event_SetSelectStage(int index)
    {
        GameManager.Instance.UserInfoData.SetData(UserDataField.SelectedStage, index);
    }
}
