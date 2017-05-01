using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class PlayerMoveLine : PlayerMoveBase, IPlayerMove{
	

	private static PlayerMoveLine _instance;

	public static PlayerMoveLine Instance(PlayerData gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveLine(gamePlayer);
		}

		return _instance;
	}

	public static void ClearInstace()
	{
		_instance = null;
	}
			
	protected PlayerMoveLine (PlayerData gamePlayer):base(gamePlayer)
	{
	}

	public void move()
	{
		Vector3 position = player.transform.position;
		if (Math.Abs (position.x) + 0.6f > GameConfig.worldWidth / 2) {
			player.direction.x = -player.direction.x;
		}
		if (Math.Abs (position.y) + 0.6f > GameConfig.worldHeight / 2) {
			player.direction.y = -player.direction.y;
		}
		player.transform.position += player.direction * player.speed;
		baseMove (player.transform.position);
	}

	public override void CheckNearBy ()
	{
		base.CheckNearBy ();
		player.parents = colliders.Where(hitCollider => hitCollider.gameObject.name != "PlayerCollider" && hitCollider.gameObject.name != "Angle").Select (hitCollider => hitCollider.gameObject.transform.parent.gameObject.GetComponent<ParentController>()).ToList();

	}


	public bool checkIfNextMove()
	{
		foreach (ParentController potentialParent in player.parents) {
			if (Vector3.Distance (potentialParent.gameObject.transform.position, player.transform.position) <= potentialParent.minDistance) {
				player.parent = potentialParent;
				return true;
			}
		}
		return false;
	}

	public void prepareNextMove()
	{
		Vector3 playerToParent = vectorToParent();
		float baseAngle = Mathf.Atan2 (playerToParent.y, playerToParent.x);
		player.enterAngle = Mathf.Atan2 (player.direction.y, player.direction.x);
		player.clockWise = false;
		if ((baseAngle <= Mathf.PI / 2 && baseAngle >= 0) || baseAngle <= -Mathf.PI / 2) {
			if (player.enterAngle >= baseAngle && player.enterAngle <= baseAngle + Mathf.PI / 2) {
				player.clockWise = true;
			}
		} else {
			if (!(player.enterAngle <= baseAngle && player.enterAngle >= baseAngle - Mathf.PI / 2)) {
				player.clockWise = true;
			}
		}
		player.angleDiff = angleBetween () * Mathf.Deg2Rad;
		player.status = "captured";
	}
		
}

