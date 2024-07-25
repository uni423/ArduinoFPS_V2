using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScoreTextObj : MonoBehaviour
{
    public void OnEnable()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);

        if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
            InGameManager.ObjectPooling.Despawn(gameObject);
        else if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
            Multi_InGameManager.ObjectPooling.Despawn(gameObject);
    }
}
