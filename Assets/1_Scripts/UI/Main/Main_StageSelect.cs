using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_StageSelect : UIBase
{
    public void OnClick_Stage(int index)
    {
        MainManager.Instance.SelectStage(index);
    }
}
