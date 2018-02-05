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
		public List<int> myActiveSkills;
		public HeroSetup (HeroType g_hero,TeamPosition g_pos, List<int> g_skills) {
			myHero = g_hero;
			myHeroPosition = g_pos;
			myActiveSkills = g_skills;
		}
	}

	[SerializeField] List<HeroSetup> myHeroSetups;
	[SerializeField] List<HeroSetup> myBossSetups;

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

	public List<HeroSetup> GetBossSetups () {
		return myBossSetups;
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
	
