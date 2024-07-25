using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class Multi_PhotonEvent : MonoBehaviourPun
{

    public void SetCombo(int playerNumber)
    {
        Player targetPlayer = null;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.GetPlayerNumber() == playerNumber)
            {
                targetPlayer = player;
            }
        }

        if (targetPlayer != null)
        {
            photonView.RPC("Event_SetCombo", targetPlayer);
        }
    }

    [PunRPC]
    public void Event_SetCombo()
    {
        if (Multi_InGameManager.Instance.playerControl != null)
            Multi_InGameManager.Instance.playerControl.SetCombo();
    }

    public void AddScore(int addScore, bool isCombo = false)
    {
        photonView.RPC("Event_AddScore", RpcTarget.All, addScore, isCombo);
    }

    [PunRPC]
    public void Event_AddScore(int addScore, bool isCombo = false)
    {
        Multi_InGameManager.Instance.score += addScore;
        UIManager.Instance.RefreshUserInfo();
        if (GameManager.Instance.platform == PlatformType.PC)
        {
            (UIManager.Instance.GetUI(UIState._PC_MultiGame_Ingame) as PC_MultiGame_Ingame).AddScoreUI(addScore, isCombo);
        }
        else if (GameManager.Instance.platform == PlatformType.Mobile)
        {
            (UIManager.Instance.GetUI(UIState._Mobile_MultiGame_Ingame) as Mobile_MultiGame_Ingame).AddScoreUI(addScore, isCombo);
        }
    }
}
