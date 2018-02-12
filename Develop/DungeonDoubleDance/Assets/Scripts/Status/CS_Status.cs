using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Status : MonoBehaviour {

	public virtual float DamageTakenMultiplier () {
		return 1;
	}

	public virtual float DamageGivenMultiplier () {
		return 1;
	}

}
