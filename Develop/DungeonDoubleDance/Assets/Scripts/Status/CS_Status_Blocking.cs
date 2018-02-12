using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Status_Blocking : CS_Status {
	public static float myDamageTakenMultiplier = 0;

	public override float DamageTakenMultiplier () {
		return myDamageTakenMultiplier;
	}
}
