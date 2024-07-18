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

    //������ ����
    public void ConnetToMaster()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    //������ ���� �Ϸ� �� �κ� ����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    //�κ� ���� �Ϸ� ��
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
