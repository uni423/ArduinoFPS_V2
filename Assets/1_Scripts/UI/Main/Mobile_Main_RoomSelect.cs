using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_Main_RoomSelect : UIBase
{
    public Mobile_Main_RoomSelect_RoomList roomList;
    public GameObject Loading;

    public override void ShowUI()
    {
        base.ShowUI();

        Loading.SetActive(false);
    }

    public void OnClick_Back()
    {
        MainManager.Instance.RoomSelectBack();
    }
}
