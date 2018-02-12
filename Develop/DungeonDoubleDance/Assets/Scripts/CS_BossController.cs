using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using JellyJoystick;

public class CS_BossController : CS_Controller {
	[TextArea(3,10)]
	[SerializeField] string myKeySheet;
	private char[] myKeySheetArray;
	private int myKeySheetArray_Index = 0;

	protected override void Init () {
		myBattlefieldSide = BattlefieldSide.Right;
		CS_GameManager.Instance.SetMyController (this, myBattlefieldSide);

		Init_Heros (CS_PlayerManager.Instance.GetBossSetups ());

		myKeySheet = myKeySheet.Replace ("\n", "");
		myKeySheetArray = myKeySheet.Replace ("|", "").ToCharArray ();
	}

	protected char GetCharFromKeySheet () {
		//get key
		char t_keyChar = myKeySheetArray [myKeySheetArray_Index];

		//move the index to the next key
		myKeySheetArray_Index++;

		//reset when all keys are played
		if (myKeySheetArray_Index == myKeySheetArray.Length) {
			myKeySheetArray_Index = 0;
		}

		return t_keyChar;
	}

	#region Beats
	public override void Beat_Center () {
		char t_keyChar = GetCharFromKeySheet ();

		switch (t_keyChar) {
		case 'A':
			OnKey (Key.A);
			break;
		case 'B':
			OnKey (Key.B);
			break;
		case 'X':
			OnKey (Key.X);
			break;
		case 'Y':
			OnKey (Key.Y);
			break;
		default:
			break;
		}
	}

	#endregion
}
