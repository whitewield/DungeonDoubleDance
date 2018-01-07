using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

[CreateAssetMenu(fileName = "SkillInfo", menuName = "Wield/SkillInfo", order = 2)]
public class SO_SkillInfo : ScriptableObject {
	public string skillName;
	public Key[] pattern;

	public SkillInfo GetSkillInfo () {
		return new SkillInfo (skillName, pattern);
	}
}

