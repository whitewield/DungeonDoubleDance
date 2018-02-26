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
	[SerializeField] RectTransform myBeatCenter;
	[SerializeField] Vector2 myBeatCenter_Size_On = Vector2.one;
	[SerializeField] Vector2 myBeatCenter_Size_Off = Vector2.one;

	[SerializeField] RectTransform myBeatLine_Right;
	[SerializeField] RectTransform myBeatLine_Left;
	[SerializeField] float myBeatLine_Distance = 200;

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
			
		} else if (myBeatProcess == BeatProcess.Back_On && myTimer > myOnBeatTime) {
			//exit the beat

			CS_GameManager.Instance.Beat_Exit ();
			myBeatProcess = BeatProcess.Back_Off;

		} else if (myTimer > myHalfBeatTime) {
			//next beat

			myBeatProcess = BeatProcess.Fore_Off;
			myTimer -= myBeatTime;

			myBeatAudioSource.PlayScheduled (AudioSettings.dspTime + (double)myHalfBeatTime);
		}
	}

//	void Update () {
//		
//	}

	void Update_Display () {

		//show beat outline
		float t_onBeatProcess = Mathf.Abs (myTimer) / myOnBeatTime;
		if (t_onBeatProcess > 1) {
			myBeatDisplay.color = myBeatDisplay_Color_Off;
			myBeatCenter.sizeDelta = myBeatCenter_Size_Off;
		} else {
			myBeatDisplay.color = (1 - t_onBeatProcess) * myBeatDisplay_Color_On + t_onBeatProcess * myBeatDisplay_Color_Off;
			myBeatCenter.sizeDelta = (1 - t_onBeatProcess) * myBeatCenter_Size_On + t_onBeatProcess * myBeatCenter_Size_Off;
		}



		//show beat line
		float t_fullProcess = -(myTimer / myBeatTime);
		if (t_fullProcess < 0) {
			t_fullProcess += 1;
		}
		myBeatLine_Right.anchoredPosition = new Vector2 (t_fullProcess * myBeatLine_Distance, 0);
		myBeatLine_Left.anchoredPosition = new Vector2 (-t_fullProcess * myBeatLine_Distance, 0);
	}


}
