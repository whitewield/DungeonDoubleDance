using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero : MonoBehaviour {

	protected Animator myAnimator;
	protected List<SkillInfo> mySkillInfoList;

	protected List<Key> myKeyRecordList = new List<Key> ();
//	protected List<SkillInfo> myPossibleSkillInfoList = new List<SkillInfo> ();

	protected CS_Controller myController;
	protected int myMaxHP;
	protected int myCurrentHP;
	protected HeroProcess myProcess;

	[SerializeField] GameObject myStatsDisplayPrefab;
	protected CS_HeroStatsDisplay myStatsDisplay;

	void Awake () {
		myAnimator = this.GetComponent<Animator> ();

		myStatsDisplay = Instantiate (myStatsDisplayPrefab, this.transform).GetComponent<CS_HeroStatsDisplay> ();
		myStatsDisplay.transform.localPosition = Vector3.zero;
	}

	public virtual void Init (CS_Controller g_controller, int g_HP, List<SkillInfo> g_skillInfos) {
		myController = g_controller;
		myProcess = HeroProcess.Idle;
		mySkillInfoList = g_skillInfos;
		myMaxHP = g_HP;
		myCurrentHP = g_HP;

		myStatsDisplay.InitSkillPattern (g_skillInfos);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual HeroProcess GetMyProcess () {
		return myProcess;
	}

	public virtual void ActionDone () {
		if (myProcess == HeroProcess.Dead)
			return;
		
		myProcess = HeroProcess.Idle;
	}
		
	public virtual void TakeDamage (int g_damage) {
		CS_Status[] t_statusArray = this.GetComponents<CS_Status> ();

		float t_damageTakenMultiplier = 1;
		foreach (CS_Status f_status in t_statusArray) {
			t_damageTakenMultiplier *= f_status.DamageTakenMultiplier ();
		}

		myCurrentHP -= Mathf.FloorToInt (g_damage * t_damageTakenMultiplier);
		if (myCurrentHP <= 0) {
			myProcess = HeroProcess.Dead;
			myCurrentHP = 0;
		}

		myStatsDisplay.SetHealth ((float)myCurrentHP / (float)myMaxHP);
//		ShowHP ();
	}

	protected virtual void Action (SkillType g_skillType) {
		
	}

	public virtual void ClearKeyRecord () {
		myKeyRecordList.Clear ();
	}

	public virtual void OnKey (Key g_key) {
		if (myProcess != HeroProcess.Idle) {
			return;
		}

		myAnimator.SetTrigger (g_key.ToString ());
		//add a function to change recorded keys

		myKeyRecordList.Add(g_key);

		int t_actionIndex = -1;
		int t_subCount = 0;
		int t_firstInUse = myKeyRecordList.Count; //the first key in the record list that is in used

		for (int i = 0; i < mySkillInfoList.Count; i++) {
			List<Key> t_keyRecord = new List<Key> (myKeyRecordList);
			int f_firstInUse = 0;

			while (t_keyRecord.Count > 0) {
				CheckSubPatternResult f_result = CheckSubPattern (t_keyRecord, mySkillInfoList [i].myPattern);

				if (f_result == CheckSubPatternResult.Sub) {
					t_subCount++;

					//its sub pattern from this skill
					//TODO: show the highlight of the keys

					break;
				} else if (f_result == CheckSubPatternResult.Same) {
					t_actionIndex = i;
					break;
				}

				t_keyRecord.RemoveAt (0);
				f_firstInUse++;
			}

			//if the recorded keys are matched with one ability, don't need to check anymore
			if (t_actionIndex != -1)
				break;

			if (f_firstInUse < t_firstInUse)
				t_firstInUse = f_firstInUse;
		}
			


		if (t_actionIndex != -1) {
			//TODO: take action
			Debug.Log (mySkillInfoList [t_actionIndex].mySkillName);
			Action (mySkillInfoList [t_actionIndex].mySkillType);
			//action done
			myKeyRecordList.Clear ();
		} else {
			myKeyRecordList.RemoveRange (0, t_firstInUse);
		}

		//debug
		string t_debug = "Record: ";
		foreach (Key f_key in myKeyRecordList) {
			t_debug = t_debug + f_key.ToString () + " ";
		}
		Debug.Log (t_debug);
	}

	protected int GetSkillDamage (SkillType g_skillType) {
		foreach (SkillInfo f_skillInfo in mySkillInfoList) {
			if (g_skillType == f_skillInfo.mySkillType)
				return f_skillInfo.myDamage;
		}

		Debug.LogError (g_skillType.ToString () + " damage not found!");
		return 0;
	}

	protected CheckSubPatternResult CheckSubPattern (List<Key> g_checkList, string g_pattern) {
		if (g_checkList.Count <= g_pattern.Length) {
			for (int j = 0; j < g_checkList.Count; j++) {
//				Debug.Log (g_pattern [j] + " " + (Key)(g_pattern [j]) + " " + g_checkList [j]);

				if ((Key)System.Enum.Parse(typeof(Key), g_pattern [j].ToString()) != g_checkList [j]) {
					return CheckSubPatternResult.Fail;
				}
				if (j + 1 == g_pattern.Length) {
					return CheckSubPatternResult.Same;
				}
			}
		} else {
			return CheckSubPatternResult.Fail;
		}

		return CheckSubPatternResult.Sub;
	}

	public enum CheckSubPatternResult {
		Fail,
		Sub,
		Same,
	}
}
