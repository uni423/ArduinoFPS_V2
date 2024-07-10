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
    }

    void Update()
    {
        if (InGameManager.IsPlaying == false)
            return;

        //이동
#if UNITY_EDITOR
        MouseRotation();
#else
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
            InGameManager.ObjectPooling.Spawn("Player_Fire", null, bulletPoint.position);

            bulletCountCur--;
            GameObject bullet = InGameManager.ObjectPooling.Spawn("Bullet");
            bullet.transform.SetPositionAndRotation(bulletPoint.position, bulletPoint.rotation);
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
            InGameManager.Instance.AddScore(comboCount, true);
    }

    #region [Coroutine]
    IEnumerator InitializeGyro()
    {
        yield return new WaitForSeconds(1f);
        GyroManager.Instance.EnableGyro();
    }
    #endregion
}
