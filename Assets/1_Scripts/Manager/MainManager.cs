using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0";

    public void Awake()
    {
        if (Utility.IsPCPlatform())
            ConnetToMaster();

        UIManager.Instance.Init();
    }

    public void ConnetToMaster()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        //PhotonNetwork.LoadLevel("LobbyScene");
    }
}
