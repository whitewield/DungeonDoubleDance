using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class CS_HeroStatsDisplay_Skill : MonoBehaviour {
	[SerializeField] Text myText_Name;
	[SerializeField] RectTransform myKeysParent;
	private List<CS_HeroStatsDisplay_Key> myKeyList = new List<CS_HeroStatsDisplay_Key> ();

	public void ShowName (string g_name) {
		myText_Name.text = g_name;
	}

	public void Highlight (int g_keyCount) {
		for (int i = 0; i < myKeyList.Count; i++) {
			if (g_keyCount > i) {
				myKeyList [i].SetHighlight (true);
			} else {
				myKeyList [i].SetHighlight (false);
			}
		}
	}

	public void AddKeys (string g_pattern) {
		for (int i = 0; i < g_pattern.Length; i++) {
			switch ((Key)System.Enum.Parse (typeof(Key), g_pattern [i].ToString ())) {
			case Key.A:
				myKeyList.Add (
					Instantiate (
						CS_GameManager.Instance.StatsKey_A, myKeysParent
					).GetComponent<CS_HeroStatsDisplay_Key> ());
				break;
			case Key.B:
				myKeyList.Add (
					Instantiate (
						CS_GameManager.Instance.StatsKey_B, myKeysParent
					).GetComponent<CS_HeroStatsDisplay_Key> ());
				break;
			case Key.X:
				myKeyList.Add (
					Instantiate (
						CS_GameManager.Instance.StatsKey_X, myKeysParent
					).GetComponent<CS_HeroStatsDisplay_Key> ());
				break;
			case Key.Y:
				myKeyList.Add (
					Instantiate (
						CS_GameManager.Instance.StatsKey_Y, myKeysParent
					).GetComponent<CS_HeroStatsDisplay_Key> ());
				break;
			}
		}
	}
}
