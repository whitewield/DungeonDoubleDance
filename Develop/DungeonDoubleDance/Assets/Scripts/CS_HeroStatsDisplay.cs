using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class CS_HeroStatsDisplay : MonoBehaviour {

	[SerializeField] RectTransform myHealthBarRectTransform;
	private float myHealthBar_DefaultWidth;
	private float myHealthBar_DefaultHeight;

	[SerializeField] RectTransform mySkillListRectTransform;
	[SerializeField] GameObject mySkillPrefab;
	private List<CS_HeroStatsDisplay_Skill> mySkillList;


	// Use this for initialization
	void Start () {
		myHealthBar_DefaultWidth = myHealthBarRectTransform.sizeDelta.x;
		myHealthBar_DefaultHeight = myHealthBarRectTransform.sizeDelta.y;
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	public void InitSkillPattern (List<SkillInfo> g_skillInfos) {
		for (int i = 0; i < g_skillInfos.Count; i++) {
			CS_HeroStatsDisplay_Skill t_skill = 
				Instantiate (mySkillPrefab, mySkillListRectTransform).GetComponent<CS_HeroStatsDisplay_Skill> ();

			t_skill.ShowText (g_skillInfos [i].mySkillName + ": " + g_skillInfos [i].myPattern);
		}
	}

	public void SetHealth (float g_percent) {
		myHealthBarRectTransform.sizeDelta = new Vector2 (
			myHealthBar_DefaultWidth * g_percent,
			myHealthBar_DefaultHeight
		);
	}
}
