using System;
using UnityEngine;

public class PlayerMoveEscape: PlayerMoveBase, IPlayerMove
{

	private static PlayerMoveEscape _instance;

	public static PlayerMoveEscape Instance(PlayerProfile gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveEscape(gamePlayer);
		}

		return _instance;
	}

	protected PlayerMoveEscape (PlayerProfile gamePlayer):base(gamePlayer)
	{
	}

	public void move()
	{
		Vector3 screenPos = worldCam.WorldToScreenPoint(player.transform.position);
		float left = screenPos.x;
		float right = Screen.width - left;
		float top = screenPos.y;
		float bottom = Screen.height - top;
		if (top < 5 || bottom < 5) {
			player.direction.y = -player.direction.y;
		}
		if (left < 5 || right < 5){
			player.direction.x = -player.direction.x;
		}
		player.transform.position += player.direction * player.speed;
		baseMove (player.transform.position);
	}

	public bool checkIfNextMove ()
	{
		return Vector3.Distance (player.parent.gameObject.transform.position, player.transform.position) > player.minDistance;
	}

	public IPlayerMove prepareNextMove ()
	{
		player.status = "line";
		return PlayerMoveLine.Instance (player);
	}

}

