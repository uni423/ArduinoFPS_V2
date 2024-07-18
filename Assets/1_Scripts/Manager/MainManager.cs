using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainManager : MonoBehaviourPunCallbacks
{
    public static MainManager Instance;
    private readonly string gameVersion = "v1.0";

    public void Awake()
    {
        Instance = this;

        if (GameManager.Instance.platform == PlatformType.PC)
            ConnetToMaster();

        UIManager.Instance.Init();
    }

    //마스터 연결
    public void ConnetToMaster()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    //마스터 연결 완료 시 로비 접속
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    //로비 접속 완료 시
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        //PhotonNetwork.LoadLevel("LobbyScene");
    }

    public void Update()
    {
        if (GameManager.Instance.platform == PlatformType.PC)
        {
        }
    }
}
