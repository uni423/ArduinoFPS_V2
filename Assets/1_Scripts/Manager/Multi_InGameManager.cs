using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class Multi_InGameManager : MonoBehaviourPunCallbacks
{
    public static Multi_InGameManager Instance;

    public static PHObjectPooling ObjectPooling { get; private set; }

    public GameObject[] mapObjArr;
    public GameObject[] playerSpawnPointArr;
    public Multi_PhotonEvent photonEvent;

    protected void Awake()
    {
        Instance = this;

        ObjectPooling = FindObjectOfType<PHObjectPooling>();

        UIManager.Instance.Init();

        for (int i = 0; i < mapObjArr.Length; i++)
            mapObjArr[i].SetActive(i == GameManager.Instance.UserInfoData.selectedStage);

        DoReady();
    }

    public void DoReady()
    {
        if (GameManager.Instance.platform == PlatformType.PC)
        {
            ObjectPooling.PrePoolInstantiate();
            PhotonNetwork.Instantiate("Multi_PC_Cam", Vector3.zero, Quaternion.identity);
        }
        else if (GameManager.Instance.platform == PlatformType.Mobile)
        {
            int playerIndex = PhotonNetwork.LocalPlayer.GetPlayerNumber() - 1;
            GameObject player = PhotonNetwork.Instantiate("Multi_Mobile_Player", playerSpawnPointArr[playerIndex].transform.position, playerSpawnPointArr[playerIndex].transform.rotation);
            //photonEvent.AddPlayer(player.GetComponent<Multi_PlayerControl>());
            player.SetActive(true); 
        }
    }

    public static void DoGameStart()
    {
        GameManager.Instance.ChangeGameStep(GameManager.Instance.platform, GameStep.Playing);

        //IsPlaying = true;
        //IsReSetting = false;
    }

}
