using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class CS_HeroStatsDisplay_Skill : MonoBehaviour {
	[SerializeField] Text myText;

	public void ShowText (string g_text) {
		myText.text = g_text;
	}
}
