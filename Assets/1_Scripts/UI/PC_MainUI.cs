using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_MainUI : UIBase
{
    public void OnClick_Stage(int index)
    {
        GameManager.Instance.UserInfoData.SetData(UserDataField.SelectedStage, index);

        SceneLoader.Load("GameScene");
    }
}
