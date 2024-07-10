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
        InGameManager.ObjectPooling.Despawn(gameObject);
    }
}
