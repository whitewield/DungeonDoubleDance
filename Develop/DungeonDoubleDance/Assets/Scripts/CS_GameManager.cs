using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_GameManager : MonoBehaviour {

	private static CS_GameManager instance = null;
	public static CS_GameManager Instance { get { return instance; } }


	private CS_Controller[] myControllers = new CS_Controller[(int)BattlefieldSide.End];
	[SerializeField] float[] myPositions_X;
	[SerializeField] float myPositions_Y;

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

	public Vector3 GetPosition (BattlefieldSide g_side, TeamPosition g_teamPos) {
		Vector3 t_pos = new Vector3 (0, myPositions_Y, 0);
		switch (g_teamPos) {
		case TeamPosition.Front:
			t_pos.x = myPositions_X [0];
			break;
		case TeamPosition.Back:
			t_pos.x = myPositions_X [1];
			break;
		case TeamPosition.All:
			t_pos.x = (myPositions_X [0] + myPositions_X [1]) * 0.5f;
			break;
		}

		switch (g_side) {
		case BattlefieldSide.Left:
			t_pos.x *= -1;
			break;
		case BattlefieldSide.Right:
			break;
		}

		return t_pos;
	}
}
