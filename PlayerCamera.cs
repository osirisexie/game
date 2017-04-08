using UnityEngine;
using System;

public class PlayerCamera: MonoBehaviour
{
	public GameObject playerObject;
	private PlayerProfile player;
	private Camera worldCam;
	private Transform targetDirection;
	private Transform speedBlock;

	private float boundryL;
	private float boundryR;
	private float boundryT;
	private float boundryB;



	void Start()
	{
		targetDirection = transform.Find ("Canvas").Find ("TargetDirection");
		speedBlock = transform.Find ("SpeedCanvas").Find ("Speed");
		worldCam = GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ();
		player = playerObject.GetComponent<PlayerProfile> ();	
		boundryB = Screen.height * player.cam.orthographicSize / worldCam.orthographicSize / 2;
		boundryL = Screen.width * player.cam.orthographicSize / worldCam.orthographicSize / 2;
		boundryT = Screen.height - boundryB;
		boundryR = Screen.width - boundryL;
	}

	void LateUpdate()
	{
		switch(player.status)
		{
		case "rotate":
		case "captured":

			cameraChase (player.parent.transform.position);
			updateSpeedIndicator ();
			break;
		case "line":
		case "escape":
		default:
			speedBlock.gameObject.SetActive (false);
			cameraChase (player.transform.position);
			break;
		}
		updateTargetIndication ();
		updateEnergy ();
	}

	private void cameraChase(Vector3 position){
		position = checkPosition (position);
		Vector3 vector = new Vector3 (position.x - transform.position.x, position.y - transform.position.y, 0);
		float rad = Mathf.Atan2 (vector.y, vector.x);
		float chaseSpeed = player.speed;
		if (player.status == "ratota")
			chaseSpeed = 2f; 
		Vector3 direction = new Vector3 (player.distanceBase * Mathf.Cos (rad), player.distanceBase * Mathf.Sin (rad), 0) * Mathf.Max(player.speed,2.5f);
		Vector3 addition;
		if(Math.Abs(direction.x) > Math.Abs(vector.x)){
			//Smooth approach Still buggy
			if (vector.magnitude < 0.5f)
				addition = vector;
			else {
				addition = vector * 0.5f / vector.magnitude;
			}
		}else{
			addition = direction;
		}
		transform.position += addition;

	}


	private Vector3 checkPosition(Vector3 position){
		Vector3 worldPosition = worldCam.WorldToScreenPoint(position);
		Vector3 newPosition = new Vector3 (0, 0, worldPosition.z);
		newPosition.x = Mathf.Max(Mathf.Min (worldPosition.x, boundryR),boundryL);
		newPosition.y = Mathf.Max (Mathf.Min (worldPosition.y, boundryT), boundryB);
		return worldCam.ScreenToWorldPoint(newPosition);
	}

	private void updateTargetIndication()
	{
		float y;
		float x;
		Vector3 direction = player.target.transform.position - player.transform.position;
		Vector3 playerPosition = player.cam.WorldToScreenPoint (player.transform.position);
		if (direction.x > 0) {
			y = playerPosition.y + (Screen.width - playerPosition.x) * direction.y / Mathf.Abs (direction.x);
		} else {
			y = playerPosition.y + (playerPosition.x) * direction.y / Mathf.Abs (direction.x);
		}

		if (direction.y > 0) {
			x = playerPosition.x + (Screen.height - playerPosition.y) * direction.x / Mathf.Abs (direction.y);
		} else {
			x = playerPosition.x + playerPosition.y * direction.x / Mathf.Abs (direction.y);
		}

		Vector3 directionPosition = new Vector3 (Mathf.Min (Screen.width-10, Mathf.Max (x, 10)), Mathf.Min (Screen.height-10, Mathf.Max (y, 10)), 0);

		directionPosition = player.cam.ScreenToWorldPoint (directionPosition);
		directionPosition.z = 0;
		targetDirection.position = directionPosition;
		if (Vector3.Distance (targetDirection.position, player.transform.position) > Vector3.Distance (player.target.transform.position, player.transform.position)) {
			targetDirection.gameObject.SetActive (false);
		} else {
			targetDirection.gameObject.SetActive (true);
		}
	}

	private void updateSpeedIndicator()
	{
		if (player.parent.name != "GameTarget") {
			speedBlock.position = player.parent.transform.position;
			speedBlock.localScale = new Vector3(1,1,0) * player.parent.GetComponent<ParentController> ().scale;
			speedBlock.gameObject.GetComponent<UnityEngine.UI.Image> ().fillClockwise = player.clockWise;
			speedBlock.gameObject.SetActive (true);
			speedBlock.gameObject.GetComponent<UnityEngine.UI.Image> ().fillAmount = player.speed/player.escapeSpeed;
		}
	}

	private void updateEnergy()
	{
		player.energyBar.GetComponent<UnityEngine.UI.Image> ().fillAmount = player.energy / 1;
	}

}

