using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerMoveRotate: PlayerMoveBase, IPlayerMove
{
	PlayerProfile player;

	private static PlayerMoveRotate _instance;

	public static PlayerMoveRotate Instance(PlayerProfile gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveRotate(gamePlayer);
		}

		return _instance;
	}

	protected PlayerMoveRotate (PlayerProfile gamePlayer):base(gamePlayer)
	{
		player = gamePlayer;
	}

	public void move()
	{
		if (player.clockWise) {
			player.angle -= player.angleAddition;
		} else {
			player.angle += player.angleAddition;
		}
		float x = player.orbit * Mathf.Cos (player.angle) + player.parent.transform.position.x;
		float y = player.orbit * Mathf.Sin (player.angle) + player.parent.transform.position.y;
		player.transform.position = new Vector3 (x, y, 0);
		baseMove (player.parent.transform.position);
	}



	public bool checkIfNextMove ()
	{
		return player.speed >= player.escapeSpeed;
	}

	public IPlayerMove prepareNextMove ()
	{
		player.direction = getDirection ();
		player.guiEnable = false;
		player.rotating = false;
		player.status = "escape";
		return PlayerMoveEscape.Instance(player);
	}
		
}

