using System;
using UnityEngine;

public class PlayerMoveEscape: PlayerMoveBase, IPlayerMove
{

	private static PlayerMoveEscape _instance;

	public static PlayerMoveEscape Instance(PlayerData gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveEscape(gamePlayer);
		}

		return _instance;
	}

	public static void ClearInstace()
	{
		_instance = null;
	}


	protected PlayerMoveEscape (PlayerData gamePlayer):base(gamePlayer)
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

	public bool checkIfNextMove ()
	{
		return Vector3.Distance (player.parent.gameObject.transform.position, player.transform.position) > player.parent.minDistance;
	}

	public void prepareNextMove ()
	{
		player.status = "line";
	}

}

