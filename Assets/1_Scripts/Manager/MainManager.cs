using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MainManager : MonoBehaviourPunCallbacks
{
    public static MainManager Instance;
    private readonly string gameVersion = "v1.0";

    #region Common

    public void Awake()
    {
        Instance = this;
        UIManager.Instance.Init();
        
        if (GameManager.Instance.platform == PlatformType.PC)
        {
            GameManager.Instance.gamePlayType = GamePlayerType.Multi;
            ConnetToMaster();
        }
        else if (GameManager.Instance.platform == PlatformType.Mobile)
        {
            GameManager.Instance.ChangeGameStep(GameStep.Mobile_Main_PlaySelect);
            UIManager.Instance.ShowUI(UIState._Mobile_Main_PlaySelect);
        }
    }

    #region Photon 

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

        if (GameManager.Instance.platform == PlatformType.PC)
            CreateRoom();
        else if (GameManager.Instance.platform == PlatformType.Mobile)
        {
            GameManager.Instance.ChangeGameStep(GameStep.Mobile_Main_RoomSelect);
            UIManager.Instance.HideUI(UIState._Mobile_Main_PlaySelect);
            UIManager.Instance.ShowUI(UIState._Mobile_Main_RoomSelect);
        }

        //PhotonNetwork.LoadLevel("LobbyScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        ChangePlayerCount();

        //���� �������� �������� �̵�
        if (playerCount >= 3)
        {
            GameManager.Instance.ChangeGameStep(GameStep.PC_Main_StageSelect);
            UIManager.Instance.HideUI(UIState._Main_PlayerConnect);
            UIManager.Instance.ShowUI(UIState._Main_StageSelect);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        ChangePlayerCount();

        //�θ� ��� �� �ο��� ����Ǿ� �÷��̾ �������� ���
        if (GameManager.Instance.gameStep == GameStep.PC_Main_StageSelect && playerCount < 3)
        {
            GameManager.Instance.ChangeGameStep(GameStep.PC_Main_WaitPlayerConnet);
            UIManager.Instance.HideUI(UIState._Main_StageSelect);
            UIManager.Instance.ShowUI(UIState._Main_PlayerConnect);
        }
    }

    #endregion

    public void SelectStage(int index)
    {
        GameManager.Instance.UserInfoData.SetData(UserDataField.SelectedStage, index);
        PhotonNetwork.LoadLevel("GameScene");
    }

    #endregion

    #region PC
    int playerCount;

    public void CreateRoom()
    {
        byte n = 10;
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;

        //���Ŀ� �� ���� ID �����ǵ��� ����
        PhotonNetwork.CreateRoom("Room101", roomOptions);

        GameManager.Instance.ChangeGameStep(GameStep.PC_Main_WaitPlayerConnet);
        UIManager.Instance.ShowUI(UIState._Main_PlayerConnect);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.LogError("Room Created");
        (UIManager.Instance.GetUI(UIState._Main_PlayerConnect) as PC_Main_PlayerConnect).SetRoomName(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.LogError("Room Created Fail");
        UIManager.Instance.ShowUI(UIState._ErrorPage);
        (UIManager.Instance.GetUI(UIState._ErrorPage) as ErrorPage).text.text = "Room Created Fail \"" + returnCode + "\"" + message + "\"";
    }

    public void ChangePlayerCount()
    {
        if (GameManager.Instance.platform != PlatformType.PC
            || PhotonNetwork.PlayerList.Length == playerCount)
            return;

        playerCount = PhotonNetwork.PlayerList.Length;

        (UIManager.Instance.GetUI(UIState._Main_PlayerConnect) as PC_Main_PlayerConnect).ChangePlayerState(playerCount);
    }

    #endregion

    #region Mobile

    public void SelectPlay(bool isSelectSolo)
    {
        if (isSelectSolo)
        {
            GameManager.Instance.gamePlayType = GamePlayerType.Solo;
            GameManager.Instance.ChangeGameStep(GameStep.Mobile_Main_StageSelect);
            UIManager.Instance.HideUI(UIState._Mobile_Main_PlaySelect);
            UIManager.Instance.ShowUI(UIState._Main_StageSelect);
        }
        else
        {
            GameManager.Instance.gamePlayType = GamePlayerType.Multi;

            ConnetToMaster();
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        GameManager.Instance.ChangeGameStep(GameStep.Mobile_Main_WaitPlayerConnet);
        UIManager.Instance.HideUI(UIState._Mobile_Main_RoomSelect);
        UIManager.Instance.ShowUI(UIState._Main_PlayerConnect);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        UIManager.Instance.ShowUI(UIState._ErrorPage);
        (UIManager.Instance.GetUI(UIState._ErrorPage) as ErrorPage).text.text = "Join Room Failed \"" + returnCode + "\"" + message + "\"";
    }
    #endregion
}
