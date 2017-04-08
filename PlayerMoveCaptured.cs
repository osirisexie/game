using System;
using UnityEngine;


public class PlayerMoveCaptured : PlayerMoveBase, IPlayerMove
{

	private static PlayerMoveCaptured _instance;

	public static PlayerMoveCaptured Instance(PlayerProfile gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveCaptured(gamePlayer);
		}

		return _instance;
	}

	protected PlayerMoveCaptured (PlayerProfile gamePlayer):base(gamePlayer)
	{
	}

	public void move()
	{
		float baseForce = 0.001f;
		float distanecRemainingAdjustment = Mathf.Pow(((Vector3.Distance (player.parent.gameObject.transform.position, player.transform.position) - player.orbit) *1.1f / (player.minDistance - player.orbit)),10) + 1f;
		float speedAdjustment = Mathf.Pow(player.speed * 1.2f/ player.maxSpeed, 10) ;
		baseForce *= (distanecRemainingAdjustment + speedAdjustment);
		float forceAngle = Mathf.Atan2 (player.parent.gameObject.transform.position.y - player.transform.position.y, player.parent.gameObject.transform.position.x - player.transform.position.x);
		Vector3 pushForce = new Vector3 (-baseForce * Mathf.Cos (forceAngle), -baseForce * Mathf.Sin (forceAngle), 0);
		Vector3 pullForce = new Vector3 (baseForce * Mathf.Cos (forceAngle), baseForce * Mathf.Sin (forceAngle), 0);
		switch (checkForceNeeded ()) 
		{
		case "push":
			player.direction += pushForce;
			if(player.clockWise)
				player.direction += Quaternion.Euler(0, 0, -90) * pushForce;
			else
				player.direction += Quaternion.Euler(0, 0, 90) * pushForce;
			break;
		case "pull":
			player.direction += pullForce;
			break;
		default:
			break;
		}

		player.transform.position += player.direction * player.speed;
		baseMove (player.transform.position);
	}

	private string checkForceNeeded(){
		float angleDiff = angleBetween ();
		if (angleDiff >= 5) {
			return "push";
		} else {
			return "pull";
		} 
	}
		

	public bool checkIfNextMove ()
	{
		return Vector3.Distance (player.parent.gameObject.transform.position, player.transform.position) <= player.orbit;
	}

	public IPlayerMove prepareNextMove ()
	{
		player.angle = Mathf.Atan2 (player.transform.position.y - player.parent.gameObject.transform.position.y, player.transform.position.x - player.parent.gameObject.transform.position.x);
		player.status = "rotate";
		player.parent.SendMessage ("Enter");
		return PlayerMoveRotate.Instance(player);
	}
}
