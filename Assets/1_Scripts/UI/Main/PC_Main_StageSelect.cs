using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Main_StageSelect : UIBase
{
    public void OnClick_Stage(int index)
    {
        MainManager.Instance.photonEvent.On_SelectStageEvnet(index);
    }
}
