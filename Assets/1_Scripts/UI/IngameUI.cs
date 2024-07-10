using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : UIBase
{
    public Image[] bulletImage;
    public Color bulletOnColor;
    public Color bulletOffColor;
    public Text scoreText;
    public Transform scoreStackParent;
    public Text timeText;

    public override void Init()
    {
        base.Init();

        UIManager.Instance.onRefreshUserInfoUI += RefreshBulletInfo;
        UIManager.Instance.onRefreshUserInfoUI += RefreshScoreInfo;
        RefreshBulletInfo();
        RefreshScoreInfo();
    }

    private void RefreshScoreInfo()
    {
        scoreText.text = string.Concat("Score: " + InGameManager.Instance.score);
    }

    private void RefreshBulletInfo()
    {
        for (int i = 0; i < InGameManager.Instance.playerControl.bulletCountMax; i++)
        {
            bulletImage[i].color = (i < InGameManager.Instance.playerControl.bulletCountCur) ? bulletOnColor : bulletOffColor;
        }
    }

    public void AddScoreUI(float point, bool isCombo)
    {
        Text scoreText = InGameManager.ObjectPooling.Spawn<Text>("Add Score Text");
        if (!isCombo)
            scoreText.text = string.Concat("+", point);
        else scoreText.text = string.Concat("Combo! +", point);
        scoreText.transform.SetParent(scoreStackParent);
    }

    public void Update()
    {
        timeText.text = string.Concat(InGameManager.Instance.gameTime.ToString("N0"), "s");
    }
}
