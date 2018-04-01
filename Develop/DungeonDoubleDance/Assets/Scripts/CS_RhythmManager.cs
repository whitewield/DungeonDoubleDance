using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class CS_RhythmManager : MonoBehaviour {

	[SerializeField] bool useTuning = false;
	[Range(-0.5f,0.5f)]
	[SerializeField] float tuningValue = 0;
	
	private static CS_RhythmManager instance = null;
	public static CS_RhythmManager Instance { get { return instance; } }

	public enum BeatProcess {
		Fore_Off,
		Fore_On,
		Back_On,
		Back_Off,
	}

	[SerializeField] int myBPM = 240;
	[Range(0,0.5f)]
	[SerializeField] float myOnBeatRatio = 0.25f;

	private float myBeatTime; // Time for each beat
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

	[SerializeField] RectTransform myBeatPoint_Window;
	[SerializeField] float myBeatPoint_Distance = 200;
	[SerializeField] int myBeatPoint_Count = 11;
	[SerializeField] GameObject myBeatPoint_Prefab;
	[SerializeField] RectTransform myBeatPoint_Parent;
	private List<GameObject> myBeatPoint_List = new List<GameObject> ();
	private int myBeatPoint_CenterIndex;

	private AudioSource myBeatAudioSource;



	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}

		myBeatTime = 60.0f / myBPM;
		Debug.Log ("Time for each beat: " + myBeatTime);

		myHalfBeatTime = myBeatTime * 0.5f;
		myOnBeatTime = myBeatTime * myOnBeatRatio;

		myTimer = -myHalfBeatTime;
		myBeatProcess = BeatProcess.Fore_Off;

		myBeatAudioSource = this.GetComponent<AudioSource> ();

		myBeatPoint_Window.sizeDelta = new Vector2 ((myBeatPoint_Distance * myOnBeatRatio * 2f), myBeatPoint_Window.sizeDelta.y);
	
		InitBeatPoints ();

//		Debug.Log (7 / 2);
	}

	void InitBeatPoints () {
		for (int i = 0; i < myBeatPoint_Count; i++) {
			GameObject t_beat = Instantiate (myBeatPoint_Prefab, myBeatPoint_Parent);
//			t_beat.GetComponentInChildren<Text> ().text = i.ToString ("0");
			myBeatPoint_List.Add (t_beat);
		}

		myBeatPoint_CenterIndex = myBeatPoint_Count / 2;
	}


//	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
	void Update () {
		myTimer += Time.deltaTime;
		//from -myHalfBeatTime to myHalfBeatTime



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

			Update_BeatPointCenter ();

//			myBeatAudioSource.PlayScheduled ((double)myHalfBeatTime);
			if (useTuning)
				myBeatAudioSource.PlayScheduled (AudioSettings.dspTime + (double)(myHalfBeatTime + tuningValue * myBeatTime));
			else
				myBeatAudioSource.PlayScheduled (AudioSettings.dspTime + (double)myHalfBeatTime);
		}


		Update_Display ();
	}

	void Update_BeatPointCenter () {
//		myBeatPoint_List [myBeatPoint_CenterIndex].GetComponent<Image> ().color = Color.red;
		myBeatPoint_CenterIndex++;
		myBeatPoint_CenterIndex %= myBeatPoint_Count;
//
//		Debug.Log (myBeatPoint_CenterIndex);
//
		int t_outBeatIndex = (myBeatPoint_Count / 2 + myBeatPoint_CenterIndex) % myBeatPoint_Count;
		myBeatPoint_List [t_outBeatIndex].GetComponent<RectTransform> ().sizeDelta = new Vector2 (40, 40);
		myBeatPoint_List [t_outBeatIndex].GetComponentInChildren<Text> ().text = "";
	}

//	void Update () {
//		
//	}

	void Update_Display () {
		if (myBeatDisplay.enabled == false)
			return;

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
//		if (t_fullProcess < 0) {
//			t_fullProcess += 1;
//		}

		// t_fullProcess (1->0), 0 = onBeat

//		Debug.Log (t_fullProcess);

		for (int i = 0; i < myBeatPoint_List.Count; i++) {

			//from -half to half

			int t_index = i - myBeatPoint_CenterIndex;
			if (t_index > myBeatPoint_Count / 2) {
				t_index = t_index - myBeatPoint_Count;
			} else if (t_index < -myBeatPoint_Count/2){
				t_index = t_index + myBeatPoint_Count;
			}

			myBeatPoint_List [i].GetComponent<RectTransform> ().anchoredPosition = 
				new Vector2 ((t_index + t_fullProcess) * myBeatPoint_Distance, 0);

//			Debug.Log(i + ":" + )
		}

//		myBeatLine_Right.anchoredPosition = new Vector2 (t_fullProcess * myBeatLine_Distance, 0);
//		myBeatLine_Left.anchoredPosition = new Vector2 (-t_fullProcess * myBeatLine_Distance, 0);
	}

	public void ShowBeat (Key g_key) {
		myBeatPoint_List [myBeatPoint_CenterIndex].GetComponent<RectTransform> ().sizeDelta = new Vector2 (80, 80);
		myBeatPoint_List [myBeatPoint_CenterIndex].GetComponentInChildren<Text> ().text = g_key.ToString ();
	}

	public float GetAccuracy () {
		float t_accuracy = 1f - Mathf.Abs (myTimer) / (myOnBeatTime);
//		Debug.LogWarning ("ACC" + t_accuracy);
		return t_accuracy;
	}
}
