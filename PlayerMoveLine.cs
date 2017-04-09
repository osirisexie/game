using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class PlayerMoveLine : PlayerMoveBase, IPlayerMove{
	

	private static PlayerMoveLine _instance;

	public static PlayerMoveLine Instance(PlayerProfile gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveLine(gamePlayer);
		}

		return _instance;
	}
			
	protected PlayerMoveLine (PlayerProfile gamePlayer):base(gamePlayer)
	{

	}

	public void move()
	{
		Vector3 screenPos = worldCam.WorldToScreenPoint(player.transform.position);
		float left = screenPos.x;
		float right = Screen.width - left;
		float top = screenPos.y;
		float bottom = Screen.height - top;
		if (top < 0  || bottom < 0) {
			player.direction.y = -player.direction.y;
		}
		if (left <0 || right < 0){
			player.direction.x = -player.direction.x;
		}
		player.transform.position += player.direction * player.speed;
		checkNearByParents ();
		baseMove (player.transform.position);
	}

	public void checkNearByParents(){
		Collider[] hitColliders = Physics.OverlapSphere (player.transform.position, 10f);
		player.parents = hitColliders.Where(hitCollider => hitCollider.gameObject.name != "PlayerCollider").Select (hitCollider => hitCollider.gameObject.transform.parent.gameObject).ToList();
	}

	public bool checkIfNextMove()
	{
		foreach (GameObject potentialParent in player.parents) {
			float parentScale = potentialParent.gameObject.GetComponent<ParentController> ().scale;
			if (Vector3.Distance (potentialParent.gameObject.transform.position, player.transform.position) <= player.minDistanceBase * parentScale) {
				player.parent = potentialParent;
				player.orbit = player.orbitBase * parentScale;
				player.minDistance = player.minDistanceBase * parentScale;
				return true;
			}
		}
		return false;
	}

	public IPlayerMove prepareNextMove()
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
		return PlayerMoveCaptured.Instance(player);
	}
		
}

