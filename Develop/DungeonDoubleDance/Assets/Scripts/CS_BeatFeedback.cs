using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class CS_BeatFeedback : MonoBehaviour {


	[System.Serializable]
	public struct BeatFeedbackSetup {
		public float myEndRate;
		public string myText;
		public Color myColor;
	}

	[SerializeField] Text myText;
	[SerializeField] float myMaxTime = 1;
	private float myMaxTimeMultiplier;
	private float myTimer;
	[SerializeField] AnimationCurve mySizeOverTime;
	[SerializeField] AnimationCurve myAlphaOverTime;
	[SerializeField] BeatFeedbackSetup[] mySetups;

	void Awake () {
		myText.text = "";
		myMaxTimeMultiplier = 1 / myMaxTime;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (myTimer <= 0)
			return;

		myTimer -= Time.deltaTime;
		if (myTimer <= 0) {
			myTimer = 0;
		}

		float t_process = 1 - (myTimer * myMaxTimeMultiplier);

		myText.transform.localScale = mySizeOverTime.Evaluate (t_process) * Vector3.one;

		Color t_color = myText.color;
		t_color.a = myAlphaOverTime.Evaluate (t_process);
		myText.color = t_color;
	}

	public void Show (float g_rate) {
		for (int i = 0; i < mySetups.Length; i++) {
			if (g_rate > mySetups [i].myEndRate) {
				Show (mySetups [i]);
				return;
			}
		}

		Show (mySetups [mySetups.Length - 1]);
	}

	public void Show (BeatFeedbackSetup g_setup) {
		myText.text = g_setup.myText;
		myText.color = g_setup.myColor;
		myTimer = myMaxTime;
	}
}
