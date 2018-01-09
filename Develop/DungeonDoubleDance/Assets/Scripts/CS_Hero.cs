using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero : MonoBehaviour {
	protected Animator myAnimator;
	protected List<SkillInfo> mySkillInfoList;


	protected List<Key> myKeyRecordList = new List<Key> ();
	protected List<SkillInfo> myPossibleSkillInfoList = new List<SkillInfo> ();
	void Awake () {
		myAnimator = this.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public virtual void OnKey (Key g_key) {
		myAnimator.SetTrigger (g_key.ToString ());
		//add a function to change recorded keys

		myKeyRecordList.Add(g_key);
		if (myPossibleSkillInfoList.Count == 0) {
			myPossibleSkillInfoList.AddRange (mySkillInfoList);
		}

		int t_actionIndex = -1;
		for (int i = 0; i < myPossibleSkillInfoList.Count; i++) {
			CheckSubPatternResult f_result = CheckSubPattern (myKeyRecordList, myPossibleSkillInfoList [i].myPattern);

			if (f_result == CheckSubPatternResult.Fail) {
				myPossibleSkillInfoList.RemoveAt (i);
				i--;
			} else if (f_result == CheckSubPatternResult.Same) {
				t_actionIndex = i;
				break;
			}
		}

		if (t_actionIndex != -1) {
			//take action

			Debug.Log (myPossibleSkillInfoList [t_actionIndex].mySkillName);

			//action done
			myKeyRecordList.Clear ();
			myPossibleSkillInfoList.Clear ();
		} else if (myPossibleSkillInfoList.Count == 0) {
			myKeyRecordList.Clear ();
			myKeyRecordList.Add (g_key);
		}

		Debug.Log (myPossibleSkillInfoList.Count + " " + mySkillInfoList.Count);

		Debug.Log ("---Start---");
		foreach (Key f_key in myKeyRecordList) {
			Debug.Log (f_key.ToString ());
		}
		Debug.Log ("----End----");
	}

	public virtual void SetMySkillInfos (List<SkillInfo> g_skillInfos) {
		mySkillInfoList = g_skillInfos;
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
