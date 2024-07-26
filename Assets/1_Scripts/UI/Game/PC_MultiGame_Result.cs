using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PC_MultiGame_Result : UIBase
{
    public SoloGame_ResultItem[] resultItems = new SoloGame_ResultItem[3];

    public Text myScoreText;
    public GameObject nameInputObj;
    public Text myNameText;

    public override void ShowUI()
    {
        base.ShowUI();

        myScoreText.text = string.Concat("Score: " + Multi_InGameManager.Instance.score);
        nameInputObj.SetActive(true);
        GetScoreBtn();
    }

    public void GetScoreBtn()
    {
        StartCoroutine(GetScores(GameManager.Instance.UserInfoData.selectedStage));
    }

    public void OnClick_SendScoreBtn()
    {
        StartCoroutine(PostScores(myNameText.text, (int)Multi_InGameManager.Instance.score));
        nameInputObj.SetActive(false);
    }

    public void OnClick_ReStart()
    {
        Multi_InGameManager.Instance.photonEvent.GameReStart();
    }

    public void OnClick_MainMenu()
    {
        Multi_InGameManager.Instance.Result_MainMenu();
    }

    #region Coroutine

    private string secretKey = "0423";
    private string addScoreURL = "http://yejun02.woobi.co.kr/addscore.php";
    private string highscoreURL = "http://yejun02.woobi.co.kr/ranking.php";

    IEnumerator GetScores(int stage)
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(highscoreURL);
        yield return hs_get.SendWebRequest();

        resultItems[0].gameObject.SetActive(false);
        resultItems[1].gameObject.SetActive(false);
        resultItems[2].gameObject.SetActive(false);

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
                for (int i = 0; i < mc.Count; i += 4)
                {
                    int stageNumber = int.Parse(splitData[i]);
                    if (stageNumber != stage)
                        continue;
                    if (splitData[i + 1] != "multi ")
                        continue;

                    Debug.LogError("Ranking Get : " + splitData[i + 1]);
                    switch (lastRanking)
                    {
                        case 0:
                            resultItems[0].gameObject.SetActive(true);
                            resultItems[0].NameText.text = splitData[i + 2];
                            resultItems[0].ScoreText.text = splitData[i + 3];
                            lastRanking = 1;
                            break;
                        case 1:
                            resultItems[1].gameObject.SetActive(true);
                            resultItems[1].NameText.text = splitData[i + 2];
                            resultItems[1].ScoreText.text = splitData[i + 3];
                            lastRanking = 2;
                            break;
                        case 2:
                            resultItems[2].gameObject.SetActive(true);
                            resultItems[2].NameText.text = splitData[i + 2];
                            resultItems[2].ScoreText.text = splitData[i + 3];
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
        form.AddField("playType", "multi");

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
    #endregion
}
