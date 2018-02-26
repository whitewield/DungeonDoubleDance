using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class CS_HeroStatsDisplay_Key : MonoBehaviour {
	[SerializeField] Image myHighlight_Image;
	[SerializeField] Color myHighlight_Color_On = Color.yellow;
	[SerializeField] Color myHighlight_Color_Off = Color.white;

	public void SetHighlight (bool g_isHighlighted) {
		if (g_isHighlighted) {
			myHighlight_Image.color = myHighlight_Color_On;
		} else {
			myHighlight_Image.color = myHighlight_Color_Off;
		}
	}


}
