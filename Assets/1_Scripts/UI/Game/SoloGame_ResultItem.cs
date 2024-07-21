using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoloGame_ResultItem : MonoBehaviour
{
    public Text NameText;
    public Text ScoreText;

    public void SetRecord(string Name, string Score)
    {
        NameText.text = Name;
        ScoreText.text = Score;
    }
}
