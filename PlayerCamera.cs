using UnityEngine;
using System;
using System.Linq;

public class PlayerCamera: MonoBehaviour
{
	private PlayerProfile player;
	private Camera cam;
	private Camera worldCam;
	private Transform targetDirection;
	private Transform speedBlock;

	private float boundryL;
	private float boundryR;
	private float boundryT;
	private float boundryB;

	private int wait = 0;
	private float initCamDiff;
	private Vector3 initPosDiff;

	private bool prepare = false;

	void Start()
	{
		cam = GetComponent<Camera> ();
		worldCam = GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ();
		player = GameObject.Find("Player").GetComponent<PlayerProfile> ();	

		cam.orthographicSize = GameConfig.camSize;
		boundryB = Screen.height * cam.orthographicSize / worldCam.orthographicSize / 2;
		boundryL = Screen.width * cam.orthographicSize / worldCam.orthographicSize / 2;
		boundryT = Screen.height - boundryB;
		boundryR = Screen.width - boundryL;
	
		initCamDiff = cam.orthographicSize - worldCam.orthographicSize;
		initPosDiff = checkPosition (player.transform.position) - Vector3.zero;
		initPosDiff.z = 0;

		//setUp
		cam.orthographicSize = worldCam.orthographicSize;
		transform.position = new Vector3 (0, 0, -2);
		targetDirection = transform.Find ("Canvas").Find ("TargetDirection");
		speedBlock = transform.Find ("SpeedCanvas").Find ("Speed");
		ResizeEnergy ();
		resizeSpeed ();
}

	private void resizeSpeed(){
		RectTransform rt = speedBlock.GetComponent<RectTransform> ();
		rt.sizeDelta = Vector2.one * 30*Screen.width/675;
	}

	private void ResizeEnergy(){
		Transform energy = transform.Find ("Canvas").Find ("Image").GetComponent<RectTransform> ();
		RectTransform rt = energy.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (Screen.width * 0.7f, 10);
		rt.localPosition = new Vector3 (-Screen.width * 0.15f, Screen.height / 2 - 5, 0);
		foreach (Transform child in energy) {
			ResizeEnergyChild (child);
		}
	}

	private void ResizeEnergyChild(Transform ts){
		RectTransform rt = ts.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (Screen.width * 0.7f-2, 8);
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
		if (player.status == "ratota")
			chaseSpeed = 2f; 
		Vector3 direction = new Vector3 (GameConfig.distanceBase * Mathf.Cos (rad), GameConfig.distanceBase * Mathf.Sin (rad), 0) * chaseSpeed;
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

