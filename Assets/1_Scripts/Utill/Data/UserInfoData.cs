using System;
using System.Collections.Generic;

using UnityEngine;

public enum UserDataField
{
    SelectedStage,
}

[System.Serializable]
public class UserInfoData
{
    public int selectedStage;

    //������ �߰��� �׻� ������ �ٿ� �߰�, ���� ������ ���� X
    public UserInfoData()
    {
        // UserInfoData �ʱ�ȭ - 1
        selectedStage = 0;
    }

    private bool IsExistInfoData() { return JsonData<UserInfoData>.IsExistDataToPath("UserInfoJson"); }
    private UserInfoData LoadData() { return JsonData<UserInfoData>.LoadDataToJson("UserInfoJson") as UserInfoData; }

    #region Init

    public void InitData()
    {
        if (IsExistInfoData())
        {
            UserInfoData loadData = LoadData();

            if (loadData != null)
            {
                selectedStage = loadData.selectedStage;
            }
        }
        else
        {
            SaveData();
        }
    }

    #endregion
    #region Get

    #endregion
    #region Set

    /// <summary>
    /// int value
    /// </summary>
    /// <param name="userDataField"></param>
    /// <param name="value"></param>
    public void SetData(UserDataField userDataField, int value)
    {
        switch (userDataField)
        {
            case UserDataField.SelectedStage:
                selectedStage = value;
                break;
        }
    }

    /// <summary>
    /// float value
    /// </summary>
    /// <param name="userDataField"></param>
    /// <param name="value"></param>
    public void SetData(UserDataField userDataField, float value)
    {
        //switch (userDataField)
        //{
        //}
    }

    /// <summary>
    /// long value
    /// </summary>
    /// <param name="userDataField"></param>
    /// <param name="value"></param>
    public void SetData(UserDataField userDataField, long value)
    {
        //switch (userDataField)
        //{
        //}
    }

    /// <summary>
    /// string value
    /// </summary>
    /// <param name="userDataField"></param>
    /// <param name="value"></param>
    public void SetData(UserDataField userDataField, bool value)
    {
        //switch (userDataField)
        //{
        //}
    }

    /// <summary>
    /// string value
    /// </summary>
    /// <param name="userDataField"></param>
    /// <param name="value"></param>
    public void SetData(UserDataField userDataField, string value)
    {
        //switch (userDataField)
        //{
        //}
    }

    #endregion
    #region Save

    public void SaveData()
    {
        JsonData<UserInfoData>.SaveDataToJson("UserInfoJson", this);
    }

    #endregion
    #region Delete

    public void DeleteData()
    {
        JsonData<UserInfoData>.DeleteData("UserInfoJson");
    }

    #endregion
}
