using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class Multi_InGameManager : MonoBehaviour
{
    public static Multi_InGameManager Instance;

    public Multi_PlayerControl playerControl;

    public UnitManager unitManager { private set; get; }

    public static PHObjectPooling PHObjectPooling { get; private set; }
    public static ObjectPooling ObjectPooling { get; private set; }

    public GameObject[] mapObjArr;
    public Transform[] playerSpawnPointArr;
    public Transform rabbitSpawnPoint;
    public Multi_PhotonEvent photonEvent;

    public static bool IsPlaying;
    public static bool IsContinue;
    public static bool IsReSetting;

    private float rabbitSpawnTimer = 0f;
    private float rabbitSpawnTime = 1.5f;
    private float rabbitSpawnRadius = 15f;
    public float score = 0;
    public float gameTime;

    protected void Awake()
    {
        Instance = this;

        PHObjectPooling = FindObjectOfType<PHObjectPooling>();
        ObjectPooling = FindObjectOfType<ObjectPooling>();

        IsPlaying = false;
        IsContinue = false;
        IsReSetting = false;

        unitManager = new UnitManager();
        unitManager.Initialize();

        UIManager.Instance.Init();

        for (int i = 0; i < mapObjArr.Length; i++)
            mapObjArr[i].SetActive(i == GameManager.Instance.UserInfoData.selectedStage);

        DoReady();

        UIManager.Instance.ChracterInit();
        DoGameStart();
    }

    public void DoReady()
    {
        PHObjectPooling.PrePoolInstantiate();
        if (GameManager.Instance.platform == PlatformType.PC)
        {
            PhotonNetwork.Instantiate("Prefabs/Multi_PC_Cam", Vector3.zero, Quaternion.identity);
        }
        else if (GameManager.Instance.platform == PlatformType.Mobile)
        {
            int playerIndex = PhotonNetwork.LocalPlayer.GetPlayerNumber() - 1;
            GameObject player = PhotonNetwork.Instantiate("Prefabs/Multi_Mobile_Player", playerSpawnPointArr[playerIndex].position, playerSpawnPointArr[playerIndex].rotation);
            playerControl = player.GetComponent<Multi_PlayerControl>();
            player.SetActive(true);
        }
    }

    public static void DoGameStart()
    {
        GameManager.Instance.ChangeGameStep(GameManager.Instance.platform, GameStep.Playing);

        IsPlaying = true;
        IsReSetting = false;

        //Instance.gameTime = 60;
        Instance.gameTime = 9999999;
    }

    private void Update()
    {
        if (IsPlaying == false)
            return;

        rabbitSpawnTimer += Time.deltaTime;
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            IsPlaying = false;
            UIManager.Instance.HideUI();
            if (GameManager.Instance.platform == PlatformType.Mobile)
                UIManager.Instance.ShowUI(UIState._Mobile_MultiGame_Result);
            else if (GameManager.Instance.platform == PlatformType.PC)
                UIManager.Instance.ShowUI(UIState._PC_MultiGame_Result);
            return;
        }

        if (GameManager.Instance.platform == PlatformType.PC)
        {
            SpawnRabbit();
            unitManager.OnUpdate(Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.platform == PlatformType.PC)
            unitManager.OnLateUpdate(Time.deltaTime);
    }

    public void SpawnRabbit()
    {
        if (rabbitSpawnTimer >= rabbitSpawnTime)
        {
            rabbitSpawnTimer -= rabbitSpawnTime;

            int randomInt = 0;

            switch (GameManager.Instance.UserInfoData.selectedStage)
            {
                case 1: randomInt = Random.Range(0, 2); break;
                case 2: randomInt = Random.Range(0, 3); break;
                case 3: randomInt = Random.Range(0, 5); break;
                default: randomInt = 0; break;
            }

            RabbitUnit rabbit = null;
            Vector3 getPoint = Random.onUnitSphere;
            getPoint.y = 0.0f;
            switch ((Unit_Type)randomInt)
            {
                case Unit_Type.Rabbit_Normal:
                    rabbit = new RabbitUnit();
                    rabbit.SetUnitTable(201);
                    break;
                case Unit_Type.Rabbit_Baby:
                    //아기 토끼의 경우 여러마리가 동시 소환 되야 하기 때문에 처리를 다르게 실행
                    StartCoroutine(BabyRbSpawn(getPoint));
                    return;
                case Unit_Type.Rabbit_Strong:
                    rabbit = new StrongRbUnit();
                    rabbit.SetUnitTable(203);
                    break;
                case Unit_Type.Rabbit_Evolve:
                    rabbit = new EvolveRbUnit();
                    rabbit.SetUnitTable(204);
                    getPoint.y = 1f;
                    break;
                case Unit_Type.Rabbit_BulkUp:
                    rabbit = new BulkUpRbUnit();
                    rabbit.SetUnitTable(205);
                    break;
            }
            rabbit.Initialize();
            rabbit.unitObject.cachedTransform.SetPositionAndRotation(
                (getPoint * rabbitSpawnRadius) + rabbitSpawnPoint.position
                , Quaternion.Euler(0, Random.Range(0, 360f), 0));

            unitManager.Regist(rabbit);
        }
    }

    #region Coroutine

    IEnumerator BabyRbSpawn(Vector3 getPoint)
    {
        int babyCount = Random.Range(3, 6);
        for (int i = 0; i < babyCount; i++)
        {
            BabyRbUnit baby = new BabyRbUnit();
            baby.SetUnitTable(202);
            baby.Initialize();
            baby.unitObject.cachedTransform.SetPositionAndRotation(
                ((getPoint * rabbitSpawnRadius) + Random.onUnitSphere) + rabbitSpawnPoint.position
                , Quaternion.Euler(0, Random.Range(0, 360f), 0));
            unitManager.Regist(baby);
            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion
}
