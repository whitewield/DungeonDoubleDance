using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Skill : MonoBehaviour {
	
	[SerializeField] protected float myDuration;
	protected float myEndTime;
	protected bool isInitialized;

	protected CS_Controller myOpponentController;
	protected TeamPosition myTargetPosition;
	protected int myDamage;

	// Use this for initialization
	//	void Start () {
	//	}

	public void Init (CS_Controller g_opponentTeam, TeamPosition g_targetPosition, int g_dmg) {
		myEndTime = myDuration + Time.timeSinceLevelLoad;
		myOpponentController = g_opponentTeam;
		myTargetPosition = g_targetPosition;
		myDamage = g_dmg;
		isInitialized = true;
	}

	// Update is called once per frame
	protected virtual void Update () {
		if (!isInitialized)
			return;

		if (Time.timeSinceLevelLoad > myEndTime) {
			DoDamage ();
			Kill ();
		}
	}

	protected virtual void DoDamage () {
		myOpponentController.TakeDamage (myTargetPosition, myDamage);
	}

	protected virtual void Kill () {
		this.gameObject.SetActive (false);
		Destroy (this.gameObject);
	}
}
