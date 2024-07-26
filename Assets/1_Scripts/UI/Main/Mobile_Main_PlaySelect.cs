using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_Main_PlaySelect : UIBase
{
    public GameObject Loading;

    public override void ShowUI()
    {
        base.ShowUI();

        Loading.SetActive(false);
    }

    public void OnClick_SoloPlay()
    {
        Loading.SetActive(true);
        MainManager.Instance.SelectPlay(true);
    }

    public void OnClick_MultiPlay()
    {
        Loading.SetActive(true);
        MainManager.Instance.SelectPlay(false);
    }
}
