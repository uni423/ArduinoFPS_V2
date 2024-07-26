using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mobile_Main_RoomSelect_RoomItem : MonoBehaviour
{
    public Text roomInfo;
    private string m_roomName;

    public void SetInfo(string roomName, int curPlayer, int maxPlayer)
    {
        m_roomName = roomName;
        roomInfo.text = roomName + "(" + curPlayer + "/" + maxPlayer + ")";
    }

    public void OnClick_RoomJoin()
    {
        (UIManager.Instance.GetUI(UIState._Mobile_Main_RoomSelect) as Mobile_Main_RoomSelect).Loading.SetActive(true);
        MainManager.Instance.JoinRoom(m_roomName);
    }
}
