using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_Main_PlaySelect : UIBase
{

    public void OnClick_SoloPlay()
    {
        MainManager.Instance.SelectPlay(true);
    }

    public void OnClick_MultiPlay()
    {
        MainManager.Instance.SelectPlay(false);
    }
}
