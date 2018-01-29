using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Controller : MonoBehaviour {

	protected List<HeroBattleInfo> myHeroBattleInfos = new List<HeroBattleInfo> ();

	protected BattlefieldSide myBattlefieldSide;

	/// <summary>
	/// Battle information for a hero.
	/// </summary>
	public class HeroBattleInfo {
		public CS_Hero myHero;
		public TeamPosition myHeroPosition;
		public List<SkillInfo> mySkillInfos;
		public HeroBattleInfo (CS_Hero g_hero, TeamPosition g_pos, List<SkillInfo> g_skills) {
			myHero = g_hero;
			myHeroPosition = g_pos;
			mySkillInfos = g_skills;
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		Init ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		Update_Position ();
	}

	protected virtual void Update_Position () {
		for (int i = 0; i < myHeroBattleInfos.Count; i++) {
			//move the hero gameObject
			myHeroBattleInfos [i].myHero.gameObject.transform.position = Vector3.Lerp (
				myHeroBattleInfos [i].myHero.gameObject.transform.position,
				CS_GameManager.Instance.GetPosition (myBattlefieldSide, myHeroBattleInfos [i].myHeroPosition),
				Constants.LERP_SPEED_MOVE * Time.deltaTime
			);
		}
	}

	protected virtual void Init () {
		
	}

	protected void OnKey (Key g_key) {
		for (int i = 0; i < myHeroBattleInfos.Count; i++) {
			myHeroBattleInfos [i].myHero.OnKey (g_key);
		}
	}

	protected CS_Hero GetHero (TeamPosition g_teamPos) {
		for (int i = 0; i < myHeroBattleInfos.Count; i++) {
			if (myHeroBattleInfos [i].myHeroPosition == g_teamPos)
				return myHeroBattleInfos [i].myHero;
		}
		return null;
	}

	public void Move () {
		for (int i = 0; i < myHeroBattleInfos.Count; i++) {
			myHeroBattleInfos [i].myHeroPosition = Constants.GetOtherPosition (myHeroBattleInfos [i].myHeroPosition);
		}
	}

	public void TakeDamage (TeamPosition g_teamPos, int g_damage) {

		//if the hero takes up all position, they will have to take damage
		CS_Hero f_hero = GetHero (TeamPosition.All);
		if (f_hero != null)
			f_hero.TakeDamage (g_damage);
		if (g_teamPos == TeamPosition.All) {
			//all positions take damage
			f_hero = GetHero (TeamPosition.Front);
			if (f_hero != null)
				f_hero.TakeDamage (g_damage);

			f_hero = GetHero (TeamPosition.Back);
			if (f_hero != null)
				f_hero.TakeDamage (g_damage);
		} else {
			//the target position take damage
			f_hero = GetHero (g_teamPos);
			if (f_hero != null)
				f_hero.TakeDamage (g_damage);
			else {
				//if the target position is empty, the other position take damage
				f_hero = GetHero (Constants.GetOtherPosition (g_teamPos));
				if (f_hero != null)
					f_hero.TakeDamage (g_damage);
			}
		}

	}
}
