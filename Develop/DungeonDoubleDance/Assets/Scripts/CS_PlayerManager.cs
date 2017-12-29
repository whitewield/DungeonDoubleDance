using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_PlayerManager : MonoBehaviour {
	
	private static CS_PlayerManager instance = null;
	public static CS_PlayerManager Instance { get { return instance; } }

	public SO_HeroBank myHeroBank;

	[System.Serializable]
	public struct HeroSetup {
		public HeroType myHero;
		public TeamPosition myHeroPosition;
		public HeroSetup (HeroType g_hero,TeamPosition g_pos) {
			myHero = g_hero;
			myHeroPosition = g_pos;
		}
	}

	[SerializeField] List<HeroSetup> myHeroSetups;

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public List<HeroSetup> GetHeroSetups () {
		return myHeroSetups;
	}

//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}
	
