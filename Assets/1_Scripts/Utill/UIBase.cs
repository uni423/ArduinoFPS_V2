using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public PlatformType platformType;
    public bool isEqualPlatform;

    public virtual void Init()
    {
        isEqualPlatform = GameManager.Instance.platform == platformType;
    }

    public virtual void CharacterInit() { }

    public virtual void ActiveOff()
    {
        gameObject.SetActive(false);
    }

    public virtual void ActiveOn()
    {
        gameObject.SetActive(true);
    }

    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideUI()
    {
        gameObject.SetActive(false);
    }
}
