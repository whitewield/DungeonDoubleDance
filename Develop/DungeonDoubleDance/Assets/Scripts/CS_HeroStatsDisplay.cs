using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HeroStatsDisplay : MonoBehaviour {

	[SerializeField] RectTransform myHealthBarRectTransform;
	private float myHealthBar_DefaultWidth;
	private float myHealthBar_DefaultHeight;

	[SerializeField] RectTransform mySkillListRectTransform;
	[SerializeField] GameObject mySkillPrefab;


	// Use this for initialization
	void Start () {
		myHealthBar_DefaultWidth = myHealthBarRectTransform.sizeDelta.x;
		myHealthBar_DefaultHeight = myHealthBarRectTransform.sizeDelta.y;
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}

	public void InitSkillPattern () {
		
	}

	public void SetHealth (float g_percent) {
		myHealthBarRectTransform.sizeDelta = new Vector2 (
			myHealthBar_DefaultWidth * g_percent,
			myHealthBar_DefaultHeight
		);
	}
}
