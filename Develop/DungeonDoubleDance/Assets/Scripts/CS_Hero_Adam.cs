using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Hero_Adam : CS_Hero {
	protected override void Action (string g_skillName) {
		switch (g_skillName) {
		case "Move":
			myController.Move ();
			break;
		}
	}
}
