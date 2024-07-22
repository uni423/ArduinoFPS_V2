using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SoloGame_Result : UIBase
{
    public SoloGame_ResultItem[] resultItems = new SoloGame_ResultItem[3];

    public Text myScoreText;
    public GameObject nameInputObj;
    public Text myNameText;

    public PhotonEvent photonEvent;

    public override void ShowUI()
    {
        base.ShowUI();

        myScoreText.text = string.Concat("Score: " + InGameManager.Instance.score);
        nameInputObj.SetActive(true);
        GetScoreBtn();
    }

    public void GetScoreBtn()
    {
        StartCoroutine(GetScores(GameManager.Instance.UserInfoData.selectedStage));
    }

    public void OnClick_SendScoreBtn()
    {
        StartCoroutine(PostScores(myNameText.text, (int)InGameManager.Instance.score));
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
                for (int i = 0; i < mc.Count; i += 3)
                {
                    int stageNumber = int.Parse(splitData[i]);
                    if (stageNumber != stage)
                        continue;
                    switch (lastRanking)
                    {
                        case 0:
                            resultItems[0].gameObject.SetActive(true);
                            resultItems[0].NameText.text = splitData[i + 1];
                            resultItems[0].ScoreText.text = splitData[i + 2];
                            lastRanking = 1;
                            break;
                        case 1:
                            resultItems[1].gameObject.SetActive(true);
                            resultItems[1].NameText.text = splitData[i + 1];
                            resultItems[1].ScoreText.text = splitData[i + 2];
                            lastRanking = 2;
                            break;
                        case 2:
                            resultItems[2].gameObject.SetActive(true);
                            resultItems[2].NameText.text = splitData[i + 1];
                            resultItems[2].ScoreText.text = splitData[i + 2];
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
    #endregion
}
