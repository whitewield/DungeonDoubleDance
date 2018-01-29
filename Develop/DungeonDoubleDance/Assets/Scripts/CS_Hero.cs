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

	void Awake () {
		myAnimator = this.GetComponent<Animator> ();
	}

	public virtual void Init (CS_Controller g_controller, int g_HP, List<SkillInfo> g_skillInfos) {
		myController = g_controller;
		myProcess = HeroProcess.Idle;
		mySkillInfoList = g_skillInfos;
		myMaxHP = g_HP;
		myCurrentHP = g_HP;
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
		
	public virtual void TakeDamage (int g_damage) {
		myCurrentHP -= g_damage;
		if (myCurrentHP <= 0) {
			myProcess = HeroProcess.Dead;
			myCurrentHP = 0;
		}

//		ShowHP ();
	}

	protected virtual void Action (string g_skillName) {
		
	}

	public virtual void OnKey (Key g_key) {
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
			Action (mySkillInfoList [t_actionIndex].mySkillName);
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

	protected CheckSubPatternResult CheckSubPattern (List<Key> g_checkList, Key[] g_pattern) {
		if (g_checkList.Count <= g_pattern.Length) {
			for (int j = 0; j < g_checkList.Count; j++) {
				if (g_pattern [j] != g_checkList [j]) {
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
