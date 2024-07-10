using UnityEngine;
using System.IO;

public static class JsonData<T>
{
    #region Check
    public static bool IsExistDataToPath(string dataName)
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, dataName + ".json");
#else
        string path = Path.Combine(Application.persistentDataPath, dataName + ".json");
#endif

        bool isExist = File.Exists(path);

        if (!isExist)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion
    #region Save

    public static void SaveDataToJson(string dataName, T data)
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, dataName + ".json");
#else
        string path = Path.Combine(Application.persistentDataPath, dataName + ".json");
#endif

        string jsonData = JsonUtility.ToJson(data, true);
#if !UNITY_EDITOR
        jsonData = AESCrypto.AESEncrypt128(jsonData);
#endif

        File.WriteAllText(path, jsonData);
    }

    #endregion
    #region Load

    public static T LoadDataToJson(string dataName)
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, dataName + ".json");
#else
        string path = Path.Combine(Application.persistentDataPath, dataName + ".json");
#endif

        string jsonData = File.ReadAllText(path);
#if !UNITY_EDITOR
        jsonData = AESCrypto.AESDecrypt128(jsonData);
#endif
        T data = JsonUtility.FromJson<T>(jsonData);

        return data;
    }

    #endregion
    #region Delete

    public static void DeleteData(string dataName)
    {
        string path = Path.Combine(Application.dataPath, dataName + ".json");

        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
            }
            catch (IOException e)
            {
                Debug.Log("UserInfoDataDelete Error => " + e);
            }
        }
    }

    #endregion
}