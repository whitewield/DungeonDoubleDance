using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using JellyJoystick;

public class CS_GameManager : MonoBehaviour {

	private static CS_GameManager instance = null;
	public static CS_GameManager Instance { get { return instance; } }


	private CS_Controller[] myControllers = new CS_Controller[(int)BattlefieldSide.End];
	[SerializeField] float[] myPositions_X;
	[SerializeField] float myPositions_Y;

	[SerializeField] GameObject myStatsKey_A;
	public GameObject StatsKey_A { get { return myStatsKey_A; } }
	[SerializeField] GameObject myStatsKey_B;
	public GameObject StatsKey_B { get { return myStatsKey_B; } }
	[SerializeField] GameObject myStatsKey_X;
	public GameObject StatsKey_X { get { return myStatsKey_X; } }
	[SerializeField] GameObject myStatsKey_Y;
	public GameObject StatsKey_Y { get { return myStatsKey_Y; } }

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
		if (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, 0, JoystickButton.START)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
		}
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

	#region Beats
	public virtual void Beat_Enter () {
		foreach (CS_Controller f_controller in myControllers) {
			f_controller.Beat_Enter ();
		}
	}

	public virtual void Beat_Center () {
		foreach (CS_Controller f_controller in myControllers) {
			f_controller.Beat_Center ();
		}
	}

	public virtual void Beat_Exit () {
		foreach (CS_Controller f_controller in myControllers) {
			f_controller.Beat_Exit ();
		}
	}
	#endregion
}
