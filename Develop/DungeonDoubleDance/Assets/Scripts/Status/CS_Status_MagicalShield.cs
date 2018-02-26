using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Status_MagicalShield : CS_Status {
	public static float myDamageTakenMultiplier = 0.5f;

	public override float DamageTakenMultiplier () {
		return myDamageTakenMultiplier;
	}
}
