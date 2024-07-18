using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BluetoothManager bluetoothManager;

    public UserInfoData UserInfoData;

    public GameStep gameStep;
    public PlatformType platform;
    public GamePlayerType gamePlayType;

    protected void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        StartCoroutineMethod(TableBase.LoadAllDataTable());

#if !UNITY_EDITOR
        bluetoothManager.Init();
#endif

        if (Utility.IsPCPlatform())
            platform = PlatformType.PC;
        else if (Utility.IsMobilePlatform())
            platform = PlatformType.Mobile;

        Initialize();

        SceneLoader.Load("MainScene");
    }

    public void Initialize()
    {
        UserInfoData = new UserInfoData();
        UserInfoData.InitData();
        UserInfoData.SaveData();
    }

    public void ChangeGameStep(GameStep changeStep)
    {
        if (gameStep != changeStep)
        {
            Debug.Log("Game Step Change : " + gameStep + " -> " + changeStep);
            gameStep = changeStep;
        }
    }

    #region Coroutine
    public static Coroutine StartCoroutineMethod(IEnumerator enumerator)
    {
        return Instance.StartCoroutine(enumerator);
    }

    public static void StopCoroutineMethod(string name)
    {
        Instance.StopCoroutine(name);
    }

    public static void StopAllCoroutineMethod()
    {
        Instance.StopAllCoroutines();
    }
#endregion
}
