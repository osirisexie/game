using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class PlayerMoveBase 
{
	PlayerProfile player;
	private float boundryL;
	private float boundryR;
	private float boundryT;
	private float boundryB;
	private Transform targetDirection;
	 
	public Camera worldCam;


	public PlayerMoveBase(PlayerProfile gamePlayer)
	{
		player = gamePlayer;
		worldCam = GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ();
		targetDirection = player.cam.transform.Find ("Canvas").Find ("TargetDirection");
//		Debug.Log (player.cam.WorldToScreenPoint(targetDirection.position));
		boundryB = Screen.height * player.cam.orthographicSize / worldCam.orthographicSize / 2;
		boundryL = Screen.width * player.cam.orthographicSize / worldCam.orthographicSize / 2;
		boundryT = Screen.height - boundryB;
		boundryR = Screen.width - boundryL;
	
	}

	public void baseMove(Vector3 position)
	{
		cameraChase (position);
		player.energyBar.GetComponent<UnityEngine.UI.Image> ().fillAmount = player.energy / 1;
		updateIndication ();

	}

	private void updateIndication()
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
			
		Vector3 directionPosition = new Vector3 (Mathf.Min (Screen.width, Mathf.Max (x, 0)), Mathf.Min (Screen.height, Mathf.Max (y, 0)), 0);
		directionPosition = player.cam.ScreenToWorldPoint (directionPosition);;
		directionPosition.z = 0;
		targetDirection.position = directionPosition;
		if (Vector3.Distance (targetDirection.position, player.transform.position) > Vector3.Distance (player.target.transform.position, player.transform.position)) {
			targetDirection.gameObject.SetActive (false);
		} else {
			targetDirection.gameObject.SetActive (true);
		}
	}

	private void cameraChase(Vector3 position){
		position = checkPosition (position);
		Transform camTrans = player.cam.transform;
		Vector3 vector = new Vector3 (position.x - camTrans.position.x, position.y - camTrans.position.y, 0);
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
		camTrans.position += addition;

	}

	private Vector3 checkPosition(Vector3 position){
		Vector3 worldPosition = worldCam.WorldToScreenPoint(position);
		Vector3 newPosition = new Vector3 (0, 0, worldPosition.z);
		newPosition.x = Mathf.Max(Mathf.Min (worldPosition.x, boundryR),boundryL);
		newPosition.y = Mathf.Max (Mathf.Min (worldPosition.y, boundryT), boundryB);
		return worldCam.ScreenToWorldPoint(newPosition);
	}

	public virtual void keyDown()
	{	
		if (player.energy > 0) {
			accelerate ();
			player.energy = Mathf.Max (0, player.energy - 0.005f);
		} else {
			bringBackSpeed ();
		}
	
	}
		
	public virtual void noKey()
	{
		bringBackSpeed ();
		player.energy = Mathf.Min (1, player.energy + 0.008f);
	}

	public void accelerate () {
		player.particle.gameObject.SetActive (true);
		adjustParticle ();
		player.speed = Mathf.Min(player.speed + player.speedAddition, player.maxSpeed);
		player.angleAddition = player.distanceBase * player.speed / player.minDistance;
	}

	public void bringBackSpeed(){
		player.particle.gameObject.SetActive (false);
		player.speed -= player.speedMinus;
		player.speed = Mathf.Max (player.speed, player.minSpeed);
		player.angleAddition = player.distanceBase * player.speed / player.minDistance;
	}

	public Vector3 vectorToParent()
	{
		return new Vector3 (player.parent.gameObject.transform.position.x - player.transform.position.x, player.parent.gameObject.transform.position.y - player.transform.position.y, 0);
	}

	public float angleBetween(){
		Vector3 playerToParent = new Vector3 (player.parent.gameObject.transform.position.x - player.transform.position.x, player.parent.gameObject.transform.position.y - player.transform.position.y, 0);
		float angleBeforeAdjust = Vector3.Angle (playerToParent, player.direction);
		float angleAdjustTo = Mathf.Asin (player.orbit / player.minDistance) * Mathf.Rad2Deg;
		return (angleAdjustTo - angleBeforeAdjust);
	}


	public Vector3 getDirection(){
		if (!player.rotating)
			return player.direction;
		else {
			Vector3 temp = new Vector3 (Mathf.Cos (player.angle) * player.distanceBase, Mathf.Sin (player.angle) * player.distanceBase);
			if (player.clockWise) {
				return Quaternion.Euler(0, 0, -90) * temp;
			}
			return Quaternion.Euler(0, 0, 90) * temp;
		}
	}

	private void adjustParticle(){
		particleDirection (getDirection ());
		changeParticleColor ();
	}

	private void particleDirection(Vector3 _direction){
		float rad = Mathf.Atan2 (-_direction.y, -_direction.x);
		float jiaodu = rad * Mathf.Rad2Deg;
		player.particle.rotation = Quaternion.Euler (new Vector3 (player.particle.localRotation.eulerAngles.x,player.particle.localRotation.eulerAngles.y , jiaodu));
	}

	private void changeParticleColor(){
		if (player.rotating) {
			float first = 0 + Mathf.Min (1, (player.speed - player.minSpeed)/(player.escapeSpeed- player.minSpeed));
			float second = 1 - Mathf.Min (1, (player.speed - player.minSpeed)/(player.escapeSpeed- player.minSpeed)); 
			player.particleSystem.startColor = new Color (first, second, 0, 1);
		} else {
			player.particleSystem.startColor = new Color (0,1,0,1);
		}
	}
}

