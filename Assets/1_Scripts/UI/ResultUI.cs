using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResultUI : UIBase
{
    public GameObject ranking_1;
    public Text nameNscore_1;
    public GameObject ranking_2;
    public Text nameNscore_2;
    public GameObject ranking_3;
    public Text nameNscore_3;

    public Text scoreText;
    public GameObject nameInputObj;
    public Text nameInputText;


    public override void ShowUI()
    {
        base.ShowUI();

        scoreText.text = string.Concat("Score: " + InGameManager.Instance.score);
        nameInputObj.SetActive(true);
        GetScoreBtn();
    }
    public void GetScoreBtn()
    {
        StartCoroutine(GetScores(GameManager.Instance.UserInfoData.selectedStage));
    }

    public void OnClick_SendScoreBtn()
    {
        StartCoroutine(PostScores(nameInputText.text, (int)InGameManager.Instance.score));
        nameInputObj.SetActive(false);
    }

    public void OnClick_ReStart()
    {
        SceneLoader.Load("GameScene");
    }

    public void OnClick_MainMenu()
    {
        SceneLoader.Load("MainScene");
    }

    private string secretKey = "0423";
    private string addScoreURL = "http://yejun02.woobi.co.kr/addscore.php";
    private string highscoreURL = "http://yejun02.woobi.co.kr/ranking.php";

    IEnumerator GetScores(int stage)
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(highscoreURL);
        yield return hs_get.SendWebRequest();

        ranking_1.SetActive(false);
        ranking_2.SetActive(false);
        ranking_3.SetActive(false);

        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: "
                    + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                int lastRanking = 0;
                for (int i = 0; i < mc.Count; i += 3)
                {
                    int stageNumber = int.Parse(splitData[i]);
                    if (stageNumber != stage)
                        continue;
                    switch (lastRanking)
                    {
                        case 0:
                            ranking_1.SetActive(true);
                            nameNscore_1.text = string.Concat(splitData[i + 1], splitData[i + 2]);
                            lastRanking = 1;
                            break;
                        case 1:
                            ranking_2.SetActive(true);
                            nameNscore_2.text = string.Concat(splitData[i + 1], splitData[i + 2]);
                            lastRanking = 2;
                            break;
                        case 2:
                            ranking_3.SetActive(true);
                            nameNscore_3.text = string.Concat(splitData[i + 1], splitData[i + 2]);
                            lastRanking = 3;
                            break;
                    }
                }
            }
        }
    }

    IEnumerator PostScores(string name, int score)
    {

        WWWForm form = new WWWForm();
        form.AddField("playerName", name);
        form.AddField("score", score);
        form.AddField("stage", GameManager.Instance.UserInfoData.selectedStage);

        UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error uploading ranking: " + www.error);
        }
        else
        {
            Debug.Log("Ranking uploaded successfully!");

            GetScoreBtn();
        }
    }

}
