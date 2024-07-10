using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldControl : MonoBehaviour
{

	InputField field;
	void Start()
	{

		GameObject inputObj = GameObject.Find("InputField");

		field = inputObj.GetComponent<InputField>();

		field.onValidateInput += delegate (string text, int charIndex, char addedChar) {
			return changeUpperCase(addedChar);
		};
	}

	char changeUpperCase(char _cha)
	{
		char tmpChar = _cha;

		string tmpString = tmpChar.ToString();

		tmpString = tmpString.ToUpper();

		tmpChar = System.Convert.ToChar(tmpString);

		return tmpChar;
	}
}