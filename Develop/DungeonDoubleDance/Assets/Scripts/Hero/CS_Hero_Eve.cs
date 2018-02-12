using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero_Eve : CS_Hero {

	private bool onMagicShield = false;

	protected override void Action (SkillType g_skillType) {
		switch (g_skillType) {
		case SkillType.EVE_Move:
			myController.Move ();
			break;
		default:
			myProcess = HeroProcess.Action;
			myAnimator.SetTrigger (g_skillType.ToString ());
			break;
		}
	}

	public override void ActionDone () {
		base.ActionDone ();

		if (this.GetComponent<CS_Status_Blocking> ()) {
			Destroy (this.GetComponent<CS_Status_Blocking> ());
		}

		if (onMagicShield) {
			onMagicShield = false;
			myController.RemoveStatus<CS_Status_MagicalShield> (TeamPosition.All);
		}
	}

	public void Block () {
		Debug.Log ("Block");

		this.gameObject.AddComponent<CS_Status_Blocking> ();
	}

	public void MagicalShield () {
		Debug.Log ("MagicalShield");
		onMagicShield = true;
		myController.ApplyStatus<CS_Status_MagicalShield> (TeamPosition.All);
	}

	public void Move () {
		Debug.Log ("Move");

	}
}
