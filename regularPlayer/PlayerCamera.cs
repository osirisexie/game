using UnityEngine;
using System;
using System.Linq;

public class PlayerCamera: MonoBehaviour
{
	private PlayerData player;
	private Camera cam;
	private Camera worldCam;
	private Transform targetDirection;
	private Transform speedBlock;

	private float boundryL;
	private float boundryR;
	private float boundryT;
	private float boundryB;
	private float ow;
	private float oh;
	private float worldOtrhographicSize;


	private int wait = 0;
	private float initCamDiff;
	private Vector3 initPosDiff;

	private bool prepare = false;

	void Awake()
	{
		ow = Screen.width;
		oh = Screen.height;

		cam = GetComponent<Camera> ();
		player = PlayerData.Instance();	
			
		worldOtrhographicSize = Math.Max (GameConfig.worldHeight / 2, GameConfig.worldWidth / cam.aspect / 2);

		initCamDiff = GameConfig.camSize - worldOtrhographicSize;
		initPosDiff = checkPosition (player.transform.position) - Vector3.zero;
		initPosDiff.z = 0;

		//setUp
		cam.orthographicSize = worldOtrhographicSize;
		transform.position = new Vector3 (0, 0, -2);	
		targetDirection = transform.Find ("Canvas").Find ("TargetDirection");
		speedBlock = transform.Find ("SpeedCanvas").Find ("Speed");
		resizeSpeed ();
	}

	private void resizeSpeed(){
		Vector3 size = player.gameObject.GetComponent<SpriteRenderer> ().bounds.size;
		size = (cam.WorldToScreenPoint (size) - cam.WorldToScreenPoint (Vector3.zero));
		if (player.status == "start") {
			size *= worldOtrhographicSize / GameConfig.camSize;
		}
		RectTransform rt = speedBlock.GetComponent<RectTransform> ();
		rt.sizeDelta = Vector2.one * (size.x + 10);
	}


	void Update(){
		if (ow != Screen.width || oh != Screen.height) {
			resizeSpeed ();
			ow = Screen.width;
			oh = Screen.height;
		}

	}
		
	void LateUpdate()
	{
		
		switch(player.status)
		{
		case "rotate":
			cameraChase (player.parent.transform.position);
			updateSpeedIndicator ();
			break;
		case "captured":
			cameraChase (player.parent.transform.position);
			break;
		case "line":
		case "escape":
			speedBlock.gameObject.SetActive (false);
			cameraChase (player.transform.position);
			break;
		case "start":
			speedBlock.gameObject.SetActive (false);
			if(prepare)showMap ();
			break;
		default:
			break;
		}
		updateTargetIndication ();
		updateEnergy ();
	}

	void PrepareMoving(){
		if(!prepare) prepare = true;
	}
		

	private Vector3 checkPosition(Vector3 position){
		Vector3 newP = new Vector3(position.x, position.y, position.z);
		if (newP.x > 0) {
			newP.x = Math.Min (newP.x, GameConfig.worldWidth / 2 - cam.orthographicSize * cam.aspect);
		}else{
			newP.x = Math.Max (newP.x, cam.orthographicSize * cam.aspect - GameConfig.worldWidth / 2);
		}
		if (newP.y > 0) {
			newP.y = Math.Min (newP.y, GameConfig.worldHeight / 2 - cam.orthographicSize );
		} else {
			newP.y = Math.Max (newP.y, cam.orthographicSize - GameConfig.worldHeight / 2);
		}
		return newP;
	}


	void StartMoving(){
		cam.orthographicSize = GameConfig.camSize;
		Vector3 np = checkPosition (player.transform.position);
		np.z = -2;
		transform.position = np;
	}

	private void cameraChase(Vector3 position){
		position = checkPosition (position);
		Vector3 vector = new Vector3 (position.x - transform.position.x, position.y - transform.position.y, 0);
		float rad = Mathf.Atan2 (vector.y, vector.x);
		float chaseSpeed = player.speed;
		Vector3 direction = new Vector3 (GameConfig.distanceBase * Mathf.Cos (rad), GameConfig.distanceBase * Mathf.Sin (rad), 0) * chaseSpeed;
		Vector3 addition;
		if(Math.Abs(direction.magnitude) > Math.Abs(vector.magnitude)){
			addition = vector;
		}else{
			addition = direction;
		}
		transform.position += addition;

	}


	private void updateTargetIndication()
	{
		float y;
		float x;
		Vector3 direction = player.target.transform.position - player.transform.position;
		Vector3 playerPosition = cam.WorldToScreenPoint (player.transform.position);
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

		directionPosition = cam.ScreenToWorldPoint (directionPosition);
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
			speedBlock.position = player.transform.position;
			speedBlock.gameObject.GetComponent<UnityEngine.UI.Image> ().fillClockwise = player.clockWise;
			speedBlock.gameObject.SetActive (true);
			speedBlock.gameObject.GetComponent<UnityEngine.UI.Image> ().fillAmount = player.speed/GameConfig.escapeSpeed;
		}
	}

	private void updateEnergy()
	{
		player.energyBar.GetComponent<UnityEngine.UI.Image> ().fillAmount = player.energy / 1;
	}

	private void showMap()
	{
		
		float camAdd = initCamDiff * GameConfig.reziseSpeed;
		Vector3 posAdd = initPosDiff * GameConfig.reziseSpeed;
		if (cam.orthographicSize > GameConfig.camSize) {
			cam.orthographicSize += camAdd;
			transform.position += posAdd;
		} else {
			player.prepared = true;
		}


	}


}

