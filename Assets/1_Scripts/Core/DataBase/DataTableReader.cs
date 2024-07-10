using UnityEngine;

public class DataTableReader
{
	public static void LoadStringToDataTable(string name, DataBase db)
	{
		try
		{
            
			TextAsset datatextAsset = Resources.Load<TextAsset>(string.Format("DataTable/{0}",name));
			if (datatextAsset == null)
			{
				Debug.LogError(name + " 파일 읽기 실패");
				return;
			}

			string[] lines = datatextAsset.text.Split('\n');
			string[] columnName = lines[0].Split(',');
			string[] columns;

			int i;
			float f;

			db.table = new DataValue[lines.Length - 1, columnName.Length];

			for (int l = 1, ll = lines.Length, r = 0; l < ll; ++l, ++r)
			{
				if (lines[l].LastIndexOf('\r') >= 0)
				{
					lines[l] = lines[l].Substring(0, lines[l].Length - 1);
				}

				columns = lines[l].Split(',');
				for (int c = 0, cc = columnName.Length; c < cc; ++c)
				{
					if (c >= columns.Length) db.table[r,c] = 0;
					else if (int.TryParse(columns[c], out i))
					{
						db.table[r,c] = i;
					}
					else if (float.TryParse(columns[c], out f))
					{
						db.table[r,c] = f;
					}
					else
					{
						if (string.IsNullOrEmpty(columns[c]))
						{
							db.table[r,c] = string.Empty;
						}
						else
						{
							if (columns[c][0] == '\"' && columns[c][columns.Length - 1] == '\"')
								columns[c] = columns[c].Substring(1, columns[c].Length - 2);
							db.table[r,c] = columns[c];
						}
					}
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError(e);
		}
	}
}
