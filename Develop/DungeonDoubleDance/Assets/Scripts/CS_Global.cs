using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {

	public enum BattlefieldSide {
		Left = 0,
		Right = 1,
		End = 2,
	}

	public enum HeroType {
		None,
		Adam,
		Slim,
	}

	public enum HeroClass {
		None,
		Front,
		Back,
		Double,
	}

	public enum TeamPosition {
		Front,
		Back,
		All,
	}

	public enum Key {
		A,
		B,
		X,
		Y,
	}

	public enum HeroProcess {
		Dead = 0,
		Idle = 1,
		Action = 2,
	}

	[System.Serializable]
	public struct SkillInfo {
		public string mySkillName;
		public int myCoolDown;
		public Key[] myPattern;
//		public SkillInfo (string g_name, int g_CD, Key[] g_pattern) {
//			mySkillName = g_name;
//			myCoolDown = g_CD;
//			myPattern = g_pattern;
//		}
	}

	public class Constants {

		public const int LERP_SPEED_MOVE = 10;
		
		public static TeamPosition GetOtherPosition (TeamPosition g_teamPosition) {
			switch (g_teamPosition) {
			case TeamPosition.Front:
				return TeamPosition.Back;
			case TeamPosition.Back:
				return TeamPosition.Front;
			default: 
				return TeamPosition.All;
			}
		}
	}


}
