using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
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
            GameObject player = null;
            if (photonEvent.multi_PlayerControls.Count <= 0)
            {
                player = PhotonNetwork.Instantiate("Multi_Mobile_Player", playerSpawnPointArr[0].transform.position, playerSpawnPointArr[0].transform.rotation);
            }
            else if (photonEvent.multi_PlayerControls.Count <= 1)
            {
                player = PhotonNetwork.Instantiate("Multi_Mobile_Player", playerSpawnPointArr[1].transform.position, playerSpawnPointArr[1].transform.rotation);
            }
            photonEvent.AddPlayer(player.GetComponent<Multi_PlayerControl>());
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
