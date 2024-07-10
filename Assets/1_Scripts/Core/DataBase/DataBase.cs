using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataValue
{
	static T get<T>(DataValue v)
	{
		return ((DataValue<T>)v).value;
	}

	public static implicit operator int(DataValue v)
	{
		if (v is DataValue<int>) return get<int>(v);
		if (v is DataValue<float>) return (int)get<float>(v);
		return 0;
	}

	public static implicit operator float(DataValue v)
	{
		if (v is DataValue<float>) return get<float>(v);
		if (v is DataValue<int>) return (float)get<int>(v);
		return 0f;
	}

	public static implicit operator bool (DataValue v) {		
		return bool.Parse (v);
	}

	public static implicit operator string(DataValue v)
	{
		return v.ToString();
	}

	public static implicit operator string[](DataValue v)
	{
		return Utility.DivideString(v);
	}

	public static implicit operator int[](DataValue v)
	{
		string[] arr =  Utility.DivideString(v);
		return System.Array.ConvertAll<string, int>(arr, Value.TryParseInt32);
	}

	public static implicit operator float[](DataValue v)
	{
		string[] arr =  Utility.DivideString(v);
		return System.Array.ConvertAll<string, float>(arr, Value.TryParseFloat);
	}

	public static implicit operator DataValue(int v)
	{
		return new DataValue<int>(v);
	}

	public static implicit operator DataValue(float v)
	{
		return new DataValue<float>(v);
	}

	public static implicit operator DataValue(string v)
	{
		return new DataValue<string>(v);
	}
}

public class DataValue<T> : DataValue
{
	public T value;

	public DataValue()
	{
		this.value = default(T);
	}

	public DataValue(T value)
	{
		this.value = value;
	}

	public override string ToString()
	{
		return this.value.ToString();
	}
}

public class DataBaseKey
{
	public string keyName;
	public int keyIndex;

	public DataBaseKey(string name, int index)
	{
		this.keyIndex = index;
		this.keyName = name;
	}
}
/*
     *      
     */
public class DataBase
{
	public DataValue[,] table = null;

	public int row { get { return table != null ? table.GetLength(0) : 0; } }
	public int col { get { return table != null ? table.GetLength(1) : 0; } }

	/*
         *      Data Table Ver
         */
	public int[] datebaseVer = null;

	/*
         *      
         */
	public string databaseName;


	/*
		 *      
		 */
	public int Serial { private set; get; }

	/*
         *      
         */
	public static Dictionary<DataBaseKey, DataBase> dataDBs = new Dictionary<DataBaseKey, DataBase>();
	public static Dictionary<string, DataBase> dicToDB = new Dictionary<string, DataBase>();

	/*
         * 
         */
	public static DataBaseKey GetDBKey(string keyname)
	{
		IEnumerator<KeyValuePair<DataBaseKey, DataBase>> iter = dataDBs.GetEnumerator();
		while (iter.MoveNext())
		{
			if (string.Equals(iter.Current.Key.keyName, keyname)) return iter.Current.Key;
		}

		return null;
	}

	/// <summary>
	/// 
	/// </summary>
	public static DataBase Get(string key)
	{
		if (dicToDB.ContainsKey(key))
			return dicToDB[key];
		else
			dicToDB.Add (key, new DataBase());

		return dicToDB[key];
	}

	/*
         * 
         */
	public static DataBase GetDB(string keyname)
	{
		IEnumerator<KeyValuePair<DataBaseKey, DataBase>> iter = dataDBs.GetEnumerator();
		while (iter.MoveNext())
		{
			if (string.Equals(iter.Current.Key.keyName, keyname)) return iter.Current.Value;
		}

		return null;
	}

	public static int GetDBCount(string keyname)
	{
		IEnumerator<KeyValuePair<DataBaseKey, DataBase>> iter = dataDBs.GetEnumerator();
		DataBase tmp = null;
		while (iter.MoveNext())
		{
			if (string.Equals(iter.Current.Key.keyName, keyname))
				tmp = iter.Current.Value;
		}
		if (tmp != null)
		{
			return tmp.row - 2;	//csv (윗태그  + 아래 /r/n  = 2)
		}
		return 0;
	}

	//-----------------------------------------------------------------------------
	//       
	//-----------------------------------------------------------------------------
	public static DataBase GetDB(DataBaseKey key, bool isAutoAdded = true)
	{
		if (dataDBs.ContainsKey(key))
		{
			return dataDBs[key];
		}
		else if (isAutoAdded)
		{
			dataDBs.Add(key, new DataBase(key));
			return dataDBs[key];
		}
		else
			return null;
	}
/*
	//===================================================================================================
	//      
	//===================================================================================================
	public static string GetValueToString(string serial, int row, int key)
	{
		DataBaseKey dbKey = DataBase.GetDBKey(serial);
		DataBase db = DataBase.GetDB(dbKey, false);
		if (db == null) return "";
		return db.table[row - dbKey.keyIndex, key];
	}

	//===================================================================================================
	//      
	//===================================================================================================
	public static int GetValueToInt32(string serial, int row, int key)
	{
		DataBaseKey dbKey = DataBase.GetDBKey(serial);
		if ((row - dbKey.keyIndex) == -90000) return 0;

		DataBase db = DataBase.GetDB(dbKey, false);
		if (db == null) return 0;
		return db.table[row - dbKey.keyIndex, key];
	}

	//===================================================================================================
	//      
	//===================================================================================================
	public static float GetValueToFloat(string serial, int row, int key)
	{
		DataBaseKey dbKey = DataBase.GetDBKey(serial);
		DataBase db = DataBase.GetDB(dbKey, false);
		if (db == null) return 0f;
		return db.table[row - dbKey.keyIndex, key];
	}

	//===================================================================================================
	//      
	//===================================================================================================
	public static string[] GetValueToArryString(string serial, int row, int key)
	{
		DataBaseKey dbKey = DataBase.GetDBKey(serial);
		DataBase db = DataBase.GetDB(dbKey, false);
		if (db == null) return null;
		return db.table[row - dbKey.keyIndex, key];
	}

	//===================================================================================================
	//      
	//===================================================================================================
	public static int[] GetValueToArryInt32(string serial, int row, int key)
	{
		DataBaseKey dbKey = DataBase.GetDBKey(serial);
		DataBase db = DataBase.GetDB(dbKey, false);
		if (db == null) return null;
		return db.table[row - dbKey.keyIndex, key];
	}

	//===================================================================================================
	//      
	//===================================================================================================
	public static float[] GetValueToArryFloat(string serial, int row, int key)
	{
		DataBaseKey dbKey = DataBase.GetDBKey(serial);
		DataBase db = DataBase.GetDB(dbKey, false);
		if (db == null) return null;
		return db.table[row - dbKey.keyIndex, key];
	}
    */
	public DataBase()
	{
	}

	//===================================================================================================
	//      버전 체크
	//===================================================================================================
	public DataBase(DataBaseKey key)
	{
		this.Serial = key.keyIndex;
	}

	//===================================================================================================
	//      버전 체크
	//===================================================================================================
	public virtual bool CheckVersion()
	{
		return true;
	}

	//===================================================================================================
	//      DOWNLOAD DB
	//===================================================================================================
	public virtual bool DownloadDB()
	{
		return true;
	}
}