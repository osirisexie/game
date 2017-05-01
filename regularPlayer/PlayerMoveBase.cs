using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class PlayerMoveBase 
{
	protected PlayerData player;
	protected Collider[] colliders;


	public PlayerMoveBase(PlayerData gamePlayer)
	{
		player = gamePlayer;
	}

	public void baseMove(Vector3 position)
	{
		CheckNearBy ();
		player.energyBar.GetComponent<UnityEngine.UI.Image> ().fillAmount = player.energy / 1;
	}

	public virtual void CheckNearBy()
	{
		colliders = Physics.OverlapSphere (player.transform.position, 0.6f);
		List<GameObject> angles = colliders.Where(hitCollider => hitCollider.gameObject.name =="Angle").Select(a=>a.gameObject).ToList();
		if (angles.Count () > 0) {
			foreach (GameObject angle in angles) {
				player.angles++;
				angle.SendMessage ("Collected");
				GameObject.Find("AngelNumber").GetComponent<UnityEngine.UI.Text>().text = player.angles+" / "+GameConfig.angels;	

			}
		}

	}


	public virtual void keyDown()
	{	
		if (player.energy > 0) {
			accelerate ();
			player.energy = Mathf.Max (0, player.energy - GameConfig.energyConsume);
		} else {
			bringBackSpeed ();
		}
	
	}
		
		
	public virtual void noKey()
	{
		bringBackSpeed ();
		player.energy = Mathf.Min (1, player.energy + GameConfig.energyRecovery);
	}

	public void accelerate () {
		player.particle.gameObject.SetActive (true);
		adjustParticle ();
		player.speed = Mathf.Min(player.speed + GameConfig.speedAddition, GameConfig.maxSpeed);
		if(player.parent)
			player.angleAddition = GameConfig.distanceBase * player.speed / player.parent.minDistance;
	}

	public void bringBackSpeed(){
		player.particle.gameObject.SetActive (false);
		player.speed -= GameConfig.speedMinus;
		player.speed = Mathf.Max (player.speed, GameConfig.minSpeed);
		if(player.parent)
			player.angleAddition = GameConfig.distanceBase * player.speed / player.parent.minDistance;
	}

	public Vector3 vectorToParent()
	{
		return new Vector3 (player.parent.gameObject.transform.position.x - player.transform.position.x, player.parent.gameObject.transform.position.y - player.transform.position.y, 0);
	}

	public float angleBetween(){
		Vector3 playerToParent = new Vector3 (player.parent.gameObject.transform.position.x - player.transform.position.x, player.parent.gameObject.transform.position.y - player.transform.position.y, 0);
		float angleBeforeAdjust = Vector3.Angle (playerToParent, player.direction);
		float angleAdjustTo = Mathf.Asin (player.parent.orbit / player.parent.minDistance) * Mathf.Rad2Deg;
		return (angleAdjustTo - angleBeforeAdjust);
	}


	public Vector3 getDirection(){
		if (player.status != "rotate" && player.status != "captured")
			return player.direction;
		else {
			Vector3 temp = player.transform.position - player.parent.transform.position;
			temp = temp * GameConfig.distanceBase / temp.magnitude;
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
			float first = 0 + Mathf.Min (1, (player.speed - GameConfig.minSpeed)/(GameConfig.escapeSpeed- GameConfig.minSpeed));
			float second = 1 - Mathf.Min (1, (player.speed - GameConfig.minSpeed)/(GameConfig.escapeSpeed- GameConfig.minSpeed)); 
			player.particleSystem.startColor = new Color (first, second, 0, 1);
		} else {
			player.particleSystem.startColor = new Color (0,1,0,1);
		}
	}
}

