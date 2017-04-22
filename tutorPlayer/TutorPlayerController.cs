using System;
using UnityEngine;

public class TutorPlayerController :PlayerController
{
	protected override void initMoveDic (PlayerProfile player)
	{
		base.initMoveDic (player);
		this.movesDic ["start"] = new TutorStart (player);
	}

	protected override void UpdateMove ()
	{
		
		GameObject.Find ("StaticTutor").SendMessage ("Left", player.status);
		base.UpdateMove ();
		try{
			if (player.parent.name != player.target.name) {
				GameObject.Find ("StaticTutor").SendMessage ("Enter", player.status);
			}
		}catch{
		}
	

	}
}

