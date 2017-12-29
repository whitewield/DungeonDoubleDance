﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Controller : MonoBehaviour {

	protected List<HeroBattleInfo> myHeroBattleInfos = new List<HeroBattleInfo> ();

	public struct HeroBattleInfo {
		public CS_Hero myHero;
		public TeamPosition myHeroPosition;
		public HeroBattleInfo (CS_Hero g_hero,TeamPosition g_pos) {
			myHero = g_hero;
			myHeroPosition = g_pos;
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		Init ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	protected virtual void Init () {
		
	}

	protected void OnKey (Key g_key) {
		for (int i = 0; i < myHeroBattleInfos.Count; i++) {
			myHeroBattleInfos [i].myHero.OnKey (g_key);
		}
	}
}
