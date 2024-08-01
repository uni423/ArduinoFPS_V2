using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_StageSelect : UIBase
{
    public void OnClick_Stage(int index)
    {
        //GameManager.Instance.UserInfoData.SetData(UserDataField.SelectedStage, index);
        if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
            SceneLoader.Load("SoloGameScene");
        else if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
        {
            MainManager.Instance.photonEvent.SetSelectStage(index);
            MainManager.Instance.photonEvent.SceneLoadEvent("MultiGameScene");
        }
    }
}
