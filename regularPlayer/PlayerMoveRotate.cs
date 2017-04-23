using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerMoveRotate: PlayerMoveBase, IPlayerMove
{

	private static PlayerMoveRotate _instance;

	public static PlayerMoveRotate Instance(PlayerProfile gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveRotate(gamePlayer);
		}

		return _instance;
	}

	public static void ClearInstace()
	{
		_instance = null;
	}


	protected PlayerMoveRotate (PlayerProfile gamePlayer):base(gamePlayer)
	{
	}

	public void move()
	{

		if (player.clockWise) {
			player.angle -= player.angleAddition;
		} else {
			player.angle += player.angleAddition;
		}
		float x = player.parent.orbit * Mathf.Cos (player.angle) + player.parent.transform.position.x;
		float y = player.parent.orbit * Mathf.Sin (player.angle) + player.parent.transform.position.y;
		player.transform.position = new Vector3 (x, y, 0);
		baseMove (player.parent.transform.position);
	}



	public bool checkIfNextMove ()
	{
		return player.speed >= GameConfig.escapeSpeed;
	}

	public void prepareNextMove ()
	{
		player.direction = getDirection ();
		player.status = "escape";
		player.parent.SendMessage ("Left");

	}
		
}

