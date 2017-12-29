using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using JellyJoystick;

public class CS_PlayerController : CS_Controller {

	private int myJoystickNumber = 1;

	protected override void Init () {
		List<CS_PlayerManager.HeroSetup> t_setups = CS_PlayerManager.Instance.GetHeroSetups ();
		for (int i = 0; i < t_setups.Count; i++) {
			GameObject f_prefab = CS_PlayerManager.Instance.myHeroBank.GetHeroPrefab (t_setups [i].myHero);
			GameObject f_heroObject = Instantiate (f_prefab, this.transform);
			myHeroBattleInfos.Add (
				new HeroBattleInfo (f_heroObject.GetComponent<CS_Hero> (), t_setups [i].myHeroPosition)
			);
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
