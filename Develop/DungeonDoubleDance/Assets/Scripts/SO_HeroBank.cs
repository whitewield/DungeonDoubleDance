using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

[CreateAssetMenu(fileName = "HeroBank", menuName = "Wield/HeroBank", order = 1)]
public class SO_HeroBank : ScriptableObject {
	public List<HeroBankInfo> Front;
	public List<HeroBankInfo> Back;
	public List<HeroBankInfo> Double;


	public HeroBankInfo emptyInfo;

	public List<HeroBankInfo> GetList (HeroClass g_class) {
		switch (g_class) {
		case HeroClass.Front:
			return Front;
		case HeroClass.Back:
			return Back;
		case HeroClass.Double:
			return Double;
		}
		Debug.LogError ("cannot find the list");
		return null;
	}


	public GameObject GetHeroPrefab (HeroType g_heroType) {
		HeroBankInfo t_heroInfo = GetHeroBankInfo (g_heroType);

		if (t_heroInfo.heroType == emptyInfo.heroType)
			return null;
		
		return t_heroInfo.prefab;
	}

	public SkillInfo GetSkillInfo (HeroType g_heroType, int g_index) {
		HeroBankInfo t_heroInfo = GetHeroBankInfo (g_heroType);

		return t_heroInfo.skillBank.GetSkillInfo (g_index);
	}

	public HeroBankInfo GetHeroBankInfo (HeroType g_heroType) {
		foreach (HeroBankInfo f_info in Front) {
			if (f_info.heroType == g_heroType)
				return f_info;
		}

		foreach (HeroBankInfo f_info in Back) {
			if (f_info.heroType == g_heroType)
				return f_info;
		}

		foreach (HeroBankInfo f_info in Double) {
			if (f_info.heroType == g_heroType)
				return f_info;
		}

		return emptyInfo;
	}
}

[System.Serializable]
public struct HeroBankInfo {
	public HeroType heroType;
	public GameObject prefab;
	public SO_SkillBank skillBank;
}