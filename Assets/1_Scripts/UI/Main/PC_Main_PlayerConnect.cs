using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Main_PlayerConnect : UIBase
{
    public Text RoomNameText;

    public GameObject Player1;
    public GameObject Player2;

    public GameObject Mobile_WaitStageSelect;

    public override void Init()
    {
        base.Init();

        RoomNameText.text = "Room Name : ";
        Player1.SetActive(false);
        Player2.SetActive(false);
        Mobile_WaitStageSelect.SetActive(false);
    }

    public override void ShowUI()
    {
        base.ShowUI();

        RoomNameText.text = "Room Name : ";
        Player1.SetActive(false);
        Player2.SetActive(false);
        Mobile_WaitStageSelect.SetActive(false);
    }

    public void SetRoomName(string Name)
    {
        RoomNameText.text = "Room Name : " + Name;
    }

    public void ChangePlayerState(int playerCount)
    {
        switch (playerCount)
        {
            case 1:
                if (Player1.activeSelf == true) Player1.SetActive(false);
                if (Player2.activeSelf == true) Player2.SetActive(false);
                break;
            case 2:
                if (Player1.activeSelf == false) Player1.SetActive(true);
                if (Player2.activeSelf == true) Player2.SetActive(false);
                break;
            case 3:
                if (Player1.activeSelf == false) Player1.SetActive(true);
                if (Player2.activeSelf == false) Player2.SetActive(true);
                if (GameManager.Instance.platform == PlatformType.Mobile && Mobile_WaitStageSelect.activeSelf == false)
                    Mobile_WaitStageSelect.SetActive(true);
                break;
        }
    }

    public void OnClick_TestStart()
    {
        GameManager.Instance.ChangeGameStep(PlatformType.PC, GameStep.PC_Main_StageSelect);
        UIManager.Instance.HideUI();
        UIManager.Instance.ShowUI(UIState._Main_StageSelect);
    }
}
