using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using JellyJoystick;

public class CS_PlayerController : CS_Controller {

	private int myJoystickNumber = 1;

	protected override void Init () {
		myBattlefieldSide = (BattlefieldSide)(myJoystickNumber - 1);
		CS_GameManager.Instance.SetMyController (this, myBattlefieldSide);

		List<CS_PlayerManager.HeroSetup> t_setups = CS_PlayerManager.Instance.GetHeroSetups ();
		for (int i = 0; i < t_setups.Count; i++) {
			//get the heroBankInfo from hero bank using the setup info
			HeroBankInfo f_heroBankInfo = CS_PlayerManager.Instance.myHeroBank.GetHeroBankInfo (t_setups [i].myHero);
			//get the prefab the heroBankInfo 
			GameObject f_prefab = f_heroBankInfo.prefab;
			// if the prefab doesn't exist, try the next one
			if (f_prefab == null)
				continue;
			//instantiate the prefab
			GameObject f_heroObject = Instantiate (f_prefab, this.transform);
			//create an array of skills
			List<SkillInfo> f_skills = new List<SkillInfo> ();
			int f_skillCount = t_setups [i].myActiveSkills.Count;
			for (int j = 0; j < f_skillCount; j++) {
				f_skills.Add (f_heroBankInfo.skillBank.GetSkillInfo (t_setups [i].myActiveSkills [j]));
			}
			f_heroObject.GetComponent<CS_Hero> ().SetMySkillInfos (f_skills);

			myHeroBattleInfos.Add (
				new HeroBattleInfo (f_heroObject.GetComponent<CS_Hero> (), t_setups [i].myHeroPosition, f_skills)
			);


			//move the hero gameObject
			myHeroBattleInfos [i].myHero.gameObject.transform.position = 
				CS_GameManager.Instance.GetPosition (myBattlefieldSide, myHeroBattleInfos [i].myHeroPosition);
		}
	}

	protected override void Update () {
		base.Update ();

		if (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myJoystickNumber, JoystickButton.A)) {
			OnKey (Key.A);
		}

		if (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myJoystickNumber, JoystickButton.B)) {
			OnKey (Key.B);
		}

		if (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myJoystickNumber, JoystickButton.X)) {
			OnKey (Key.X);
		}

		if (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myJoystickNumber, JoystickButton.Y)) {
			OnKey (Key.Y);
		}
	}

}
