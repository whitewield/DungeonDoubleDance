using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;
using JellyJoystick;

public class CS_PlayerController : CS_Controller {

	[SerializeField] Transform myBar_Transform;
	private float myBar_ValueMax = 100;
	private float myBar_ValueCurrent;
	[SerializeField] float myBar_ValueDecreaseSpeed = 10;
	[SerializeField] float myBar_ValueOnBeatMultiplier = 10;
	[SerializeField] float myBar_ValueOffBeatDecrease = 20;

	private int myJoystickNumber = 1;

	[SerializeField] CS_BeatFeedback myBeatFeedback;

	protected override void Init () {
		myBattlefieldSide = (BattlefieldSide)(myJoystickNumber - 1);
		CS_GameManager.Instance.SetMyController (this, myBattlefieldSide);

		Init_Heros (CS_PlayerManager.Instance.GetHeroSetups ());

		UpdateBarDisplay ();
	}

	protected override void Update () {
		base.Update ();

		Update_Input ();

		Update_Bar ();
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

	protected override void DoOnBeat (Key g_key) {
		float t_accuracy = CS_RhythmManager.Instance.GetAccuracy ();
//		Debug.LogWarning ("DoOnBeat" + t_accuracy);
		myBeatFeedback.Show (t_accuracy);
		ModifyBarValue (t_accuracy * t_accuracy * myBar_ValueOnBeatMultiplier);

		CS_RhythmManager.Instance.ShowBeat (g_key);
	}

	protected override void DoOffBeat () {
		float t_accuracy = CS_RhythmManager.Instance.GetAccuracy ();
//		Debug.LogWarning ("OOOOOOOOOOOFF" + t_accuracy);
		myBeatFeedback.Show (-1);
		ModifyBarValue (-myBar_ValueOffBeatDecrease);
	}

	private void ModifyBarValue (float g_value) {
		myBar_ValueCurrent += g_value;
		if (myBar_ValueCurrent > myBar_ValueMax)
			myBar_ValueCurrent = myBar_ValueMax;
		else if (myBar_ValueCurrent < 0)
			myBar_ValueCurrent = 0;
	}

	private void Update_Bar () {
		ModifyBarValue (-Time.deltaTime * myBar_ValueDecreaseSpeed);
		UpdateBarDisplay ();
	}

	private void UpdateBarDisplay () {
		myBar_Transform.localScale = new Vector3 (myBar_ValueCurrent / myBar_ValueMax, 1, 1);
	}

}

