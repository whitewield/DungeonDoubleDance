using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

[CreateAssetMenu(fileName = "HeroBank", menuName = "Wield/HeroBank", order = 2)]
public class SO_HeroBank : ScriptableObject {
	public List<HeroInfo> Front;
	public List<HeroInfo> Back;
	public List<HeroInfo> Double;


	public HeroInfo emptyInfo;

	public List<HeroInfo> GetList (HeroClass g_class) {
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
		HeroInfo t_heroInfo = GetHeroInfo (g_heroType);

		if (t_heroInfo.heroType == emptyInfo.heroType)
			return null;
		
		return t_heroInfo.prefab;
	}

	public HeroInfo GetHeroInfo (HeroType g_heroType) {
		foreach (HeroInfo f_info in Front) {
			if (f_info.heroType == g_heroType)
				return f_info;
		}

		foreach (HeroInfo f_info in Back) {
			if (f_info.heroType == g_heroType)
				return f_info;
		}

		foreach (HeroInfo f_info in Double) {
			if (f_info.heroType == g_heroType)
				return f_info;
		}

		return emptyInfo;
	}
}

[System.Serializable]
public struct HeroInfo {
	public HeroType heroType;
	public GameObject prefab;
}