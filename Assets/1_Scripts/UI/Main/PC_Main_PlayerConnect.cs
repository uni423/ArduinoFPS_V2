using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Main_PlayerConnect : UIBase
{
    public Text RoomNameText;

    public GameObject Player1;
    public GameObject Player2;

    public override void ShowUI()
    {
        base.ShowUI();

        RoomNameText.text = "Room Name : ";
        Player1.SetActive(false);
        Player2.SetActive(false);
    }

    public void SetRoomName(string Name)
    {
        RoomNameText.text = "Room Name : " + Name;
    }

    public void ChangePlayerState(int playerCount)
    {
        switch(playerCount)
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
                break;
        }
    }

    public void OnClick_TestStart()
    {
        GameManager.Instance.ChangeGameStep(GameStep.PC_Main_StageSelect);
        UIManager.Instance.HideUI(UIState._Main_PlayerConnect);
        UIManager.Instance.ShowUI(UIState._Main_StageSelect);
    }
}
