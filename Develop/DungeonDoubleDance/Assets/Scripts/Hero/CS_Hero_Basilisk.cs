using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero_Basilisk : CS_Hero {
	protected override void Action (SkillType g_skillType) {
		switch (g_skillType) {
		default:
			myProcess = HeroProcess.Action;
			myAnimator.SetTrigger (g_skillType.ToString ());
			break;
		}
	}

	public void Devour () {
		Debug.Log ("Devour");

		CS_GameManager.Instance.GetOpponentController (myController).TakeDamage (
			Global.TeamPosition.Front, 
			GetSkillDamage (SkillType.BSK_Devour)
		);
	}

	public void FangStrike () {
		Debug.Log ("FangStrike");

		CS_GameManager.Instance.GetOpponentController (myController).TakeDamage (
			Global.TeamPosition.Back, 
			GetSkillDamage (SkillType.BSK_FangStrike)
		);
	}

	public void SerpentAcid () {
		Debug.Log ("SerpentAcid");

		CS_GameManager.Instance.GetOpponentController (myController).TakeDamage (
			Global.TeamPosition.All, 
			GetSkillDamage (SkillType.BSK_SerpentAcid)
		);
	}
}
