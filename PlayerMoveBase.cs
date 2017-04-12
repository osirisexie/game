using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class PlayerMoveBase 
{
	protected PlayerProfile player;
	 
	protected Camera worldCam;


	public PlayerMoveBase(PlayerProfile gamePlayer)
	{
		player = gamePlayer;
		worldCam = GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ();
	}

	public void baseMove(Vector3 position)
	{
		player.energyBar.GetComponent<UnityEngine.UI.Image> ().fillAmount = player.energy / 1;

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
		if (player.status != "rotate" && player.status != "captured")
			return player.direction;
		else {
			Vector3 temp = player.transform.position - player.parent.transform.position;
			temp = temp * player.distanceBase / temp.magnitude;
			if (player.clockWise) {
				return Quaternion.Euler(0, 0, -90) * temp;
			}
			return Quaternion.Euler(0, 0, 90) * temp;
		}
	}

	private void adjustParticle(){
		particleDirection (getDirection ());
//		changeParticleColor ();
	}

	private void particleDirection(Vector3 _direction){
		float rad = Mathf.Atan2 (-_direction.y, -_direction.x);
		float jiaodu = rad * Mathf.Rad2Deg;
		player.particle.rotation = Quaternion.Euler (new Vector3 (player.particle.localRotation.eulerAngles.x,player.particle.localRotation.eulerAngles.y , jiaodu));
	}

	private void changeParticleColor(){
		if (player.status == "rotate") {
			float first = 0 + Mathf.Min (1, (player.speed - player.minSpeed)/(player.escapeSpeed- player.minSpeed));
			float second = 1 - Mathf.Min (1, (player.speed - player.minSpeed)/(player.escapeSpeed- player.minSpeed)); 
			player.particleSystem.startColor = new Color (first, second, 0, 1);
		} else {
			player.particleSystem.startColor = new Color (0,1,0,1);
		}
	}
}

