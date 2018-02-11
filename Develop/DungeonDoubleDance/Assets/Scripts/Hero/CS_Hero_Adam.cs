using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero_Adam : CS_Hero {
	protected override void Action (SkillType g_skillType) {
		switch (g_skillType) {
		case SkillType.ADM_SkewerSlash:
			myProcess = HeroProcess.Action;
			myAnimator.SetTrigger (SkillType.ADM_SkewerSlash.ToString ());
			break;
		case SkillType.ADM_Fireball:
			myProcess = HeroProcess.Action;
			myAnimator.SetTrigger (SkillType.ADM_Fireball.ToString ());
			break;
		case SkillType.ADM_FireStrike:
			myController.Move ();
			break;
		}
	}

	public void SkewerSlash () {
		Debug.Log ("SkewerSlash!!!!");
		CS_GameManager.Instance.GetOpponentController (myController).TakeDamage (
			Global.TeamPosition.Front, 
			GetSkillDamage (SkillType.ADM_SkewerSlash)
		);
	}

	public void Fireball () {
		Debug.Log ("FireBallllllll");
		CS_GameManager.Instance.GetOpponentController (myController).TakeDamage (
			Global.TeamPosition.Front, 
			GetSkillDamage (SkillType.ADM_Fireball)
		);
	}

	public void FireStrike () {
		Debug.Log ("FireStrike");

	}
}
