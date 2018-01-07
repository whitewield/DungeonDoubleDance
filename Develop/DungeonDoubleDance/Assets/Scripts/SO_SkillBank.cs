using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

[CreateAssetMenu(fileName = "SkillBank", menuName = "Wield/SkillBank", order = 2)]
public class SO_SkillBank : ScriptableObject {
	public SkillInfo[] mySkillInfos;

	public SkillInfo[] GetSkillInfos () {
		return mySkillInfos;
	}

	public SkillInfo GetSkillInfo (int g_index) {
		return mySkillInfos [g_index];
	}
}

