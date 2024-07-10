using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utility
{
 	 /// <summary>
	/// 칼럼의 값을 | 기호 기준으로 쪼개서 배열에 담는다.
  	/// </summary>
  	/// <returns>The string.</returns>
  	/// <param name="word">Word.</param>
    public static string[] DivideString(string word)
    {
        char[] tok = new char[1] { '|' };

        string[] arrWord = word.Split(tok);

        return arrWord;
    }

}
