using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum HERO_DATA_TABLE
{
	HDT_UNITINFO,
    HDT_SKILLINFO,
    HDT_ABNORMALINFO,
	HDT_MAX,
}


public class DataBaseInfo
{
	public static string[] tbName = 
        {
            "DB/TEXT/UnitInfo",
            "DB/TEXT/ItemInfo",
        };

	public static string[] DataBaseString = 
        {
            "DB_UNIT_INFO",
            "DB_ITEM_INFO",
        };

	public static int[] DataBaseIndex = 
        {
            100,           
            200,
        };
	
	public const string DBS_UNIT_INFO = "DB_UNIT_INFO";
	public const int DBI_UNIT_INFO = 100;

    public const string DBS_SKILL_INFO = "DB_SKILL_INFO";
    public const int DBI_SKILL_INFO = 200;
    
}
