using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_GameManager : MonoBehaviour {

	private static CS_GameManager instance = null;
	public static CS_GameManager Instance { get { return instance; } }


	private CS_Controller[] myControllers = new CS_Controller[(int)BattlefieldSide.End];
	[SerializeField] Transform[] myPositions_Left;
	[SerializeField] Transform[] myPositions_Right;

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMyController (CS_Controller g_controller, BattlefieldSide g_side) {
		myControllers [(int)g_side] = g_controller;
	}

	public void SetMyController (CS_Controller g_controller, int g_sideInt) {
		myControllers [g_sideInt] = g_controller;
	}

	public CS_Controller GetOpponentController (CS_Controller g_controller) {
		return myControllers [1 - System.Array.IndexOf (myControllers, g_controller)];
	}
}
