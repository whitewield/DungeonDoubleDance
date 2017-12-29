using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Hero : MonoBehaviour {
	protected Animator myAnimator;
	void Awake () {
		myAnimator = this.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void OnKey (Key g_key) {
		myAnimator.SetTrigger (g_key.ToString ());
	}
}
