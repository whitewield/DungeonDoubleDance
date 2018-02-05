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

		Init_Heros (CS_PlayerManager.Instance.GetHeroSetups ());
	}

	protected override void Update () {
		base.Update ();

		Update_Input ();
	}

	protected void Update_Input () {
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
