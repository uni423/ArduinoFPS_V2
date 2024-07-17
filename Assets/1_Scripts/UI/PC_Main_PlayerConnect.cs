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

    public void ChangePlayerState(int index, bool isJoined)
    {
        if (index == 1 && Player1.activeSelf == isJoined)
            Player1.SetActive(isJoined);
        else if (index == 2 && Player2.activeSelf == isJoined)
            Player2.SetActive(isJoined);
    }
}
