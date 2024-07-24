using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_MultiGame_Ingame : UIBase
{
    public Text scoreText;
    public Transform scoreStackParent;
    public Text timeText;

    public override void Init()
    {
        base.Init();
        UIManager.Instance.onRefreshUserInfoUI += RefreshScoreInfo;
        RefreshScoreInfo();
    }

    private void RefreshScoreInfo()
    {
        scoreText.text = string.Concat("Score: " + Multi_InGameManager.Instance.score);
    }

    public void AddScoreUI(float point, bool isCombo)
    {
        Text scoreText = Multi_InGameManager.ObjectPooling.Spawn<Text>("Add Score Text");
        if (!isCombo)
            scoreText.text = string.Concat("+", point);
        else scoreText.text = string.Concat("Combo! +", point);
        scoreText.transform.SetParent(scoreStackParent);
    }

    public void Update()
    {
        timeText.text = string.Concat(Multi_InGameManager.Instance.gameTime.ToString("N0"), "s");
    }
}
