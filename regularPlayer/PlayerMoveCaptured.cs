using System;
using UnityEngine;


public class PlayerMoveCaptured : PlayerMoveBase, IPlayerMove
{

	private static PlayerMoveCaptured _instance;
	private bool angleAdjusted = false;


	public static PlayerMoveCaptured Instance(PlayerProfile gamePlayer)
	{
		if (_instance == null)
		{
			_instance = new PlayerMoveCaptured(gamePlayer);
		}

		return _instance;
	}

	public static void ClearInstace()
	{
		_instance = null;
	}


	protected PlayerMoveCaptured (PlayerProfile gamePlayer):base(gamePlayer)
	{

	}

	public void move()
	{
		if (currentAngle () <= 95 && !angleAdjusted) {
			if (player.clockWise) {
				player.direction = Quaternion.Euler (0, 0, 3) * player.direction;
			} else {
				player.direction = Quaternion.Euler (0, 0, -3) * player.direction;
			}	
			player.transform.position += player.direction * player.speed;

		} else {
			angleAdjusted = true;
			Vector3 pTop = (player.transform.position - player.parent.transform.position);
			if (player.clockWise) {
				pTop = Quaternion.Euler (0, 0, -player.angleAddition * Mathf.Rad2Deg) * pTop * (pTop.magnitude - 0.01f*player.parent.GetComponent<ParentController>().scale/0.5f)/pTop.magnitude;
			} else {
				pTop = Quaternion.Euler (0, 0, +player.angleAddition * Mathf.Rad2Deg) * pTop * (pTop.magnitude - 0.01f*player.parent.GetComponent<ParentController>().scale/0.5f)/pTop.magnitude;
			}	
			player.transform.position = player.parent.transform.position + pTop;
		}

		baseMove (player.transform.position);
	}

	private float currentAngle()
	{
		Vector3 playerToParent = player.parent.transform.position - player.transform.position;
		return Vector3.Angle (playerToParent, player.direction);
	}

	private string checkForceNeeded(){
		float angleDiff = angleBetween ();
		if (angleDiff >= 2) {
			return "push";
		} else {
			return "pull";
		} 
	}
		

	public bool checkIfNextMove ()
	{
		return Vector3.Distance (player.parent.gameObject.transform.position, player.transform.position) <= player.parent.orbit;
	}

	public void prepareNextMove ()
	{
		if(player.parent.name == "GameTarget"){
			player.stopwatch.Stop ();
			GameObject.Find ("DataKeeper").GetComponent<DataKeeper> ().time = (double)player.stopwatch.ElapsedMilliseconds/1000;
			Application.LoadLevel ("Complete");		
		}
		player.angle = Mathf.Atan2 (player.transform.position.y - player.parent.gameObject.transform.position.y, player.transform.position.x - player.parent.gameObject.transform.position.x);
		player.status = "rotate";
		player.parent.SendMessage ("Enter");
		angleAdjusted = false;
//		if ((SharedData.currentLevel == 0) && player.parent.name != "GameTarget") {
//			GameObject.Find ("CaptureTutor").transform.Find ("ShowEscape").gameObject.SetActive (true);
//		}

	}
}
