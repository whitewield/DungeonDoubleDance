using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero_Basilisk : CS_Hero {
	protected override void Action (SkillType g_skillType) {
		switch (g_skillType) {
		case SkillType.BSK_Devour:
			myController.Move ();
			break;
//		case SkillType.BSK_FangStrike:
//			myController.Move ();
//			break;
//		case SkillType.BSK_SerpentAcid:
//			myController.Move ();
//			break;
		}
	}
}
