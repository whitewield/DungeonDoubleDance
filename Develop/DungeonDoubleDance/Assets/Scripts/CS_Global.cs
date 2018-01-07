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

	[System.Serializable]
	public struct SkillInfo {
		public string mySkillName;
		public Key[] myPattern;
		public SkillInfo (string g_name, Key[] g_pattern) {
			mySkillName = g_name;
			myPattern = g_pattern;
		}
	}

	public class Constants {
		
	}

}
