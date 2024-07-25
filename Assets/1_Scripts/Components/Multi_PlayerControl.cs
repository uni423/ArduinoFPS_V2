using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Multi_PlayerControl : MonoBehaviourPunCallbacks, IPunObservable
{
    #region [Value]

    public GameObject mine;
    public GameObject yours;

    public Animator animator;

    #region [��]

    public Transform bulletPoint;

    public int bulletCountMax;
    public int bulletCountCur;
    public int comboCount;
    public float comboTimeMax;
    public float comboTimeCur;

    private bool isTrigger;
    private bool isReload;

    public bool isUsePump;
    public float pumpValue;
    public float pumpAddValue;
    public Mobile_MultiGame_Ingame multiIngameUI;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] reloadSFX;
    public AudioClip[] fireSFX;

    #endregion

    #region [�̵�]
    public float turnSpeed = 4.0f; // ���콺 ȸ�� �ӵ�

    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )

    private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);
    public Vector3 quaternion;

    public Transform mineTransform; 
    public Transform yoursTransform; //��ݽŸ� �����̵��� �ϱ� ���� �Ϲݽ� ������
    public Transform spineTransform; //��ݽŸ� �����̵��� �ϱ� ���� ��ݽ� �̵���
    #endregion

    #endregion

    public void Start()
    {
        mine.SetActive(photonView.IsMine);
        yours.SetActive(!photonView.IsMine);

        StartCoroutine(InitializeGyro());

        bulletCountMax = 5;
        comboCount = 0;
        comboTimeMax = 5f;
        comboTimeCur = 0f;
        OnReload();

        isUsePump = !GameManager.Instance.bluetoothManager.IsConnected;
        multiIngameUI = (UIManager.Instance.GetUI(UIState._Mobile_MultiGame_Ingame) as Mobile_MultiGame_Ingame);
        multiIngameUI.pump.gameObject.SetActive(isUsePump);
    }

    void Update()
    {
        if (Multi_InGameManager.IsPlaying == false) return;
        if (!photonView.IsMine) return;

        //�̵�
#if UNITY_EDITOR
        MouseRotation();
#else
        mineTransform.localRotation = Quaternion.Euler(quaternion) * (GyroManager.Instance.GetGyroRotation() * baseRotation);
        Quaternion totalRotation = Quaternion.Euler(quaternion) * (GyroManager.Instance.GetGyroRotation() * baseRotation);

        yoursTransform.localRotation = Quaternion.Euler(0, totalRotation.eulerAngles.y, 0);
        spineTransform.localRotation = Quaternion.Euler(totalRotation.eulerAngles.x, 0, totalRotation.eulerAngles.z);
        
#endif
        //����
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

        //�޺�
        if (comboCount > 0)
        {
            comboTimeCur += Time.deltaTime;
            if (comboTimeCur >= comboTimeMax)
            {
                //�޺� ����
                comboCount = 0;
                comboTimeCur = 0f;
            }
        }

        //��Ʈ�ѷ� ���� �� ���� ���
        if (isUsePump)
        {
            pumpValue = multiIngameUI.pump.value;
            if (pumpValue <= (multiIngameUI.pump.maxValue * 0.1f) && !isReload)
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
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(yoursTransform.localRotation);
            stream.SendNext(spineTransform.localRotation);
        }
        else
        {
            yoursTransform.localRotation = (Quaternion)stream.ReceiveNext();
            spineTransform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }

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
            Multi_InGameManager.PHObjectPooling.PoolInstantiate("Effect/Player_Fire", bulletPoint.position, Quaternion.identity);

            //Animation
            animator.SetTrigger("Fire");

            bulletCountCur--;

            Multi_InGameManager.PHObjectPooling.PoolInstantiate("Bullet", bulletPoint.position, bulletPoint.rotation);
            UIManager.Instance.RefreshUserInfo();
        }
    }

    public void OnReload()
    {
        if (bulletCountCur < bulletCountMax)
        {
            //Sound
            audioSource.clip = reloadSFX[Random.Range(0, reloadSFX.Length)];
            audioSource.Play();

            //Animation
            animator.SetTrigger("Reload");

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
    }

    public void SetCombo()
    {
        comboCount++;
        comboTimeCur = 0f;
        if (comboCount > 1)
        {
            if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
                Multi_InGameManager.Instance.photonEvent.AddScore(comboCount, true);
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
