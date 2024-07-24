using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region [Value]
    #region [총]
    public Transform bulletPoint;

    public int bulletCountMax;
    public int bulletCountCur;
    public int comboCount;
    public float comboTimeMax;
    public float comboTimeCur;

    public bool isUsePump;
    public float pumpValue;
    public float pumpAddValue;
    public IngameUI ingameUI;
    public Mobile_MultiGame_Ingame multiIngameUI;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] reloadSFX;
    public AudioClip[] fireSFX;

    #endregion
    #region [이동]
    public float turnSpeed = 4.0f; // 마우스 회전 속도

    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )

    private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);
    public Vector3 quaternion;
    #endregion
    #endregion

    void Start()
    {
        StartCoroutine(InitializeGyro());

        bulletCountMax = 5;
        comboCount = 0;
        comboTimeMax = 5f;
        comboTimeCur = 0f;
        OnReload();

        isUsePump = !GameManager.Instance.bluetoothManager.IsConnected;
        if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
            multiIngameUI = (UIManager.Instance.GetUI(UIState._Mobile_MultiGame_Ingame) as Mobile_MultiGame_Ingame);
        else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
            ingameUI = (UIManager.Instance.GetUI(UIState._SoloGame_Ingame) as IngameUI);
    }

    void Update()
    {
        if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
            if (Multi_InGameManager.IsPlaying == false) return;
        if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
            if (InGameManager.IsPlaying == false) return;

        //이동
#if UNITY_EDITOR
        MouseRotation();
#else
        Debug.Log("PlayerControl Update" + GyroManager.Instance.GetGyroRotation());
        transform.localRotation = Quaternion.Euler(quaternion) * (GyroManager.Instance.GetGyroRotation() * baseRotation);
#endif

        //공격
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Home))
        {
            //Debug.Log("Fire");
            OnFire();
        }
        else if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Fire");
                OnFire();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
            OnReload();

        //콤보
        if (comboCount > 0)
        {
            comboTimeCur += Time.deltaTime;
            if (comboTimeCur >= comboTimeMax)
            {
                //콤보 종료
                comboCount = 0;
                comboTimeCur = 0f;
            }
        }

        //컨트롤러 없을 때 펌프 사용
        if (isUsePump)
        {
            if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
            {
                pumpValue = multiIngameUI.pump.value;
                if (pumpValue <= 100 && !isReload)
                {
                    isReload = true;
                    OnReload();
                }
                if (pumpValue < multiIngameUI.pump.maxValue)
                {
                    float max = multiIngameUI.pump.maxValue;
                    if (max > multiIngameUI.pump.maxValue) max = multiIngameUI.pump.maxValue;
                    pumpValue += Mathf.Lerp(0, max, Time.deltaTime * pumpAddValue);
                    multiIngameUI.pump.value = pumpValue;
                }
                if (pumpValue >= multiIngameUI.pump.maxValue && isReload)
                {
                    isReload = false;
                }
            }
            else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
            {
                pumpValue = ingameUI.pump.value;
                if (pumpValue <= 100 && !isReload)
                {
                    isReload = true;
                    OnReload();
                }
                if (pumpValue < ingameUI.pump.maxValue)
                {
                    float max = ingameUI.pump.maxValue;
                    if (max > ingameUI.pump.maxValue) max = ingameUI.pump.maxValue;
                    pumpValue += Mathf.Lerp(0, max, Time.deltaTime * pumpAddValue);
                    ingameUI.pump.value = pumpValue;
                }
                if (pumpValue >= ingameUI.pump.maxValue && isReload)
                {
                    isReload = false;
                }
            }
        }
    }

    private bool isTrigger;
    private bool isReload;
    public void Buletooth(string command)
    {
        Debug.Log("Buletooth" + command);
        switch (command)
        {
            case "trigger On":
                if (!isTrigger)
                {
                    isTrigger = true;
                    OnFire();
                }
                break;
            case "trigger Off":
                isTrigger = false;
                break;
            case "Reload PUSH & Reload":
                if (!isReload)
                {
                    isReload = true;
                    OnReload();
                }
                break;
            case "Reload PULL":
                isReload = false;
                break;
        }
    }

    public void OnFire()
    {
        if (bulletCountCur > 0)
        {
            //Sound
            audioSource.clip = fireSFX[Random.Range(0, fireSFX.Length)];
            audioSource.Play();
            //Effect

            if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
                Multi_InGameManager.PHObjectPooling.PoolInstantiate("Effect/Player_Fire", bulletPoint.position, Quaternion.identity);
            else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
                InGameManager.ObjectPooling.Spawn("Player_Fire", null, bulletPoint.position);

            bulletCountCur--;
            if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
                Multi_InGameManager.PHObjectPooling.PoolInstantiate("Bullet", bulletPoint.position, bulletPoint.rotation);
            else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
            {
                GameObject bullet = InGameManager.ObjectPooling.Spawn("Bullet");
                bullet.transform.SetPositionAndRotation(bulletPoint.position, bulletPoint.rotation);
            }
            UIManager.Instance.RefreshUserInfo();
        }
    }

    public void OnReload()
    {
        if (bulletCountCur < bulletCountMax)
        {
            audioSource.clip = reloadSFX[Random.Range(0, reloadSFX.Length)];
            audioSource.Play();
            bulletCountCur = bulletCountMax;
            UIManager.Instance.RefreshUserInfo();
        }
    }

    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        //transform.Rotate(Input.gyro.rotationRateUnbiased.x, Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
        //Debug.Log(Input.gyro.attitude); // attitude has data now
    }

    public void SetCombo()
    {
        comboCount++;
        comboTimeCur = 0f;
        if (comboCount > 1)
        {
            if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
                Multi_InGameManager.Instance.AddScore(comboCount, true);
            else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
                InGameManager.Instance.AddScore(comboCount, true);
        }
    }

    #region [Coroutine]
    IEnumerator InitializeGyro()
    {
        yield return new WaitForSeconds(1f);
        GyroManager.Instance.EnableGyro();
    }
    #endregion
}
