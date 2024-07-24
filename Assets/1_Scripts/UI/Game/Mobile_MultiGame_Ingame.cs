using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mobile_MultiGame_Ingame : UIBase
{
    public Image[] bulletImage;
    public Color bulletOnColor;
    public Color bulletOffColor;
    public Text scoreText;
    public Transform scoreStackParent;
    public Text timeText;

    public Slider pump;

    public override void CharacterInit()
    {
        base.CharacterInit();

        UIManager.Instance.onRefreshUserInfoUI += RefreshBulletInfo;
        UIManager.Instance.onRefreshUserInfoUI += RefreshScoreInfo;
        RefreshBulletInfo();
        RefreshScoreInfo();
    }

    private void RefreshScoreInfo()
    {
        scoreText.text = string.Concat("Score: " + Multi_InGameManager.Instance.score);
    }

    private void RefreshBulletInfo()
    {
        if (!isEqualPlatform) return;

        for (int i = 0; i < Multi_InGameManager.Instance.playerControl.bulletCountMax; i++)
        {
            bulletImage[i].color = (i < Multi_InGameManager.Instance.playerControl.bulletCountCur) ? bulletOnColor : bulletOffColor;
        }
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
