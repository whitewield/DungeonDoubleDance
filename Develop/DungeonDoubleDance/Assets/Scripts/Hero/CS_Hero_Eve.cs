using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero_Eve : CS_Hero {
	protected override void Action (SkillType g_skillType) {
		switch (g_skillType) {
//		case SkillType.EVE_Block:
//			myController.Move ();
//			break;
//		case SkillType.EVE_MagicalShield:
//			myController.Move ();
//			break;
		case SkillType.EVE_Move:
			myController.Move ();
			break;
		}
	}

	public void Block () {
		Debug.Log ("Block");

	}

	public void MagicalShield () {
		Debug.Log ("MagicalShield");

	}

	public void Move () {
		Debug.Log ("Move");

	}
}
