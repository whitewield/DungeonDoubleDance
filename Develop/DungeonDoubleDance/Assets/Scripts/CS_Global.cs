using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {


	public enum HeroType {
		None,
		Adam,
		Eve,
		//===Boss===//
		Basilisk
	}

	public enum SkillType {
		None,
		_____Adam_____ = 100,
		ADM_SkewerSlash = 101,
		ADM_Fireball = 102,
		ADM_FireStrike = 103,

		_____Eve_____ = 200,
		EVE_Block = 201,
		EVE_MagicalShield = 202,
		EVE_Move = 203,

		_____Basilisk_____ = 10100,
		BSK_Devour = 10101,
		BSK_FangStrike = 10102,
		BSK_SerpentAcid = 10103,
	}

	public enum BattlefieldSide {
		Left = 0,
		Right = 1,
		End = 2,
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
		public SkillType mySkillType;
		public int myDamage;
		public int myCoolDown;
		public string myPattern;
//		public SkillInfo (string g_name, int g_CD, Key[] g_pattern) {
//			mySkillName = g_name;
//			myCoolDown = g_CD;
//			myPattern = g_pattern;
//		}
	}

	public class Constants {

		public const int LERP_SPEED_MOVE = 10;

		public static float GetRandomSign () {
			return Mathf.Sign(Random.Range (0, 2) - 0.5f);
		}
		
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
