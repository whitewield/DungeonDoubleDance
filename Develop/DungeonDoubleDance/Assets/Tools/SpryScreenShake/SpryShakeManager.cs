using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hang{
	namespace SpryShake{

		public enum SpryShakeMode {
			Position,
			Rotation,
		}

		public class SpryShakeManager : MonoBehaviour {

			private static SpryShakeManager instance = null;
			public static SpryShakeManager Instance { get { return instance; } }

			private List<SpryShakeSetup> myShakeList = new List<SpryShakeSetup>();

			void Awake () {
				if (instance != null && instance != this) {
					Destroy(this.gameObject);
				} else {
					instance = this;
				}

				DontDestroyOnLoad (this.gameObject);
			}

			void Update () {

				for (int i = 0; i < myShakeList.Count; i++) {
					if (myShakeList [i].Update () == false) {
						myShakeList.RemoveAt (i);
						i--;
					}
				}
			}

			public void CreateShake (
				Transform g_target, 
				SpryShakeMode g_mode = SpryShakeMode.Position, 
				float g_duration = 1, 
				Vector3 g_intensity = default(Vector3), 
				Vector3 g_speed = default(Vector3)
			) {

				for (int i = 0; i < myShakeList.Count; i++) {
					if (myShakeList [i].GetTargetTransform () == g_target) {
						myShakeList [i].UpdateSetup (g_mode, g_duration, g_intensity, g_speed);
						return;
					}
				}

				SpryShakeSetup t_shake = new SpryShakeSetup (g_target, g_mode, g_duration, g_intensity, g_speed);
				myShakeList.Add (t_shake);
			}

		}

		public class SpryShakeSetup {
			private Transform myTarget;
			private SpryShakeMode myMode;
			private float myDuration;
			private Vector3 mySpeed;
			private Vector3 myIntensity;

			public SpryShakeSetup (
				Transform g_target, 
				SpryShakeMode g_mode = SpryShakeMode.Position, 
				float g_duration = 1, 
				Vector3 g_intensity = default(Vector3), 
				Vector3 g_speed = default(Vector3)
			) {
				myTarget = g_target;
				myMode = g_mode;
				myDuration = g_duration;
				myIntensity = g_intensity;
				mySpeed = g_speed;

				myDefaultPosition = g_target.position;
				myTimer = 0;
			}

			private Vector3 myDefaultPosition;
			private float myTimer;

			public void UpdateSetup (
				SpryShakeMode g_mode = SpryShakeMode.Position, 
				float g_duration = 1, 
				Vector3 g_intensity = default(Vector3), 
				Vector3 g_speed = default(Vector3)
			) {
				myMode = g_mode;
				myDuration = g_duration;
				myIntensity = g_intensity;
				mySpeed = g_speed;
			}

			public bool Update () {
				return Update_Position ();
			}

			private bool Update_Position () {

				Vector3 t_CurrentIntensity = myIntensity * (1 - myTimer / myDuration); 

				myTarget.transform.position = 
					myDefaultPosition + new Vector3 (
						t_CurrentIntensity.x * Mathf.Sin (myTimer * mySpeed.x), 
						t_CurrentIntensity.y * Mathf.Sin (myTimer * mySpeed.y), 
						t_CurrentIntensity.y * Mathf.Sin (myTimer * mySpeed.z)
					);

				myTimer += Time.deltaTime;
				
				// time out
				if (myTimer > myDuration) {
					myTarget.position = myDefaultPosition;
					return false;
				}

				return true;
			}

			public Transform GetTargetTransform () {
				return myTarget;
			}
		}


	}
}