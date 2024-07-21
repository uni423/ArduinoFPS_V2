using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonEvent : MonoBehaviourPun
{
    public void On_SelectStageEvnet(int index)
    { 
        if (photonView.IsMine)
        {
            photonView.RPC("Event_StageStart", RpcTarget.AllViaServer, index);
        }
    }

    [PunRPC]
    public void Event_StageStart(int index)
    {
        GameManager.Instance.UserInfoData.SetData(UserDataField.SelectedStage, index);
        PhotonNetwork.LoadLevel("GameScene");
    }
}
