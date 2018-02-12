using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_RhythmManager : MonoBehaviour {

	public enum BeatProcess {
		Fore_Off,
		Fore_On,
		Back_On,
		Back_Off,
	}

	[SerializeField] int myBPM = 120;
	[Range(0,0.5f)]
	[SerializeField] float myOnBeatRatio = 0.25f;

	private float myBeatTime;
	private float myHalfBeatTime;
	private float myOnBeatTime;

	private float myTimer;
	private BeatProcess myBeatProcess;

	[SerializeField] Image myBeatDisplay;
	[SerializeField] Color myBeatDisplay_Color_On = Color.white;
	[SerializeField] Color myBeatDisplay_Color_Off = Color.black;

	private AudioSource myBeatAudioSource;

	void Awake () {
		myBeatTime = 60.0f / myBPM;
		Debug.Log ("Time for each beat: " + myBeatTime);

		myHalfBeatTime = myBeatTime * 0.5f;
		myOnBeatTime = myBeatTime * myOnBeatRatio;

		myTimer = -myHalfBeatTime;
		myBeatProcess = BeatProcess.Fore_Off;

		myBeatAudioSource = this.GetComponent<AudioSource> ();
	}

//	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
	void Update () {
		myTimer += Time.deltaTime;

		Update_Display ();

		if (myBeatProcess == BeatProcess.Fore_Off && myTimer > -myOnBeatTime) {
			//enter the beat

			CS_GameManager.Instance.Beat_Enter ();
			myBeatProcess = BeatProcess.Fore_On;

		} else if (myBeatProcess == BeatProcess.Fore_On && myTimer > 0) {
			//center of the beat

			CS_GameManager.Instance.Beat_Center ();
			myBeatProcess = BeatProcess.Back_On;

			myBeatAudioSource.Play ();
			
		} else if (myBeatProcess == BeatProcess.Back_On && myTimer > myOnBeatTime) {
			//exit the beat

			CS_GameManager.Instance.Beat_Exit ();
			myBeatProcess = BeatProcess.Back_Off;

		} else if (myTimer > myHalfBeatTime) {
			//next beat

			myBeatProcess = BeatProcess.Fore_Off;
			myTimer -= myBeatTime;
		}
	}

	void Update_Display () {
		float t_process = Mathf.Abs (myTimer);
		if (t_process > myOnBeatTime) {
			myBeatDisplay.color = myBeatDisplay_Color_Off;
			return;
		}

		t_process = t_process / myOnBeatTime;
		myBeatDisplay.color = (1 - t_process) * myBeatDisplay_Color_On + t_process * myBeatDisplay_Color_Off;
	}


}
