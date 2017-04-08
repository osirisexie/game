using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
	
	IPlayerMove moveController;
	PlayerProfile player;
	MicrophoneInput microphone;


	void Start()
	{
		Camera worldCam = GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ();
		worldCam.enabled = false;
		player = GetComponent<PlayerProfile> ();
		microphone = new MicrophoneInput (player);
//		microphone.StartMicListener ();
		moveController = PlayerMoveLine.Instance (player);
	}

	void Update()
	{	
		
//		if (microphone.AnalyzeSound () > -30 || Input.GetKey (KeyCode.UpArrow)) {
//			moveController.keyDown ();
//		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			moveController.keyDown ();
		}
		else {
			moveController.noKey ();
		}
		moveController.move ();
		if (moveController.checkIfNextMove ()) 
		{
			moveController =  moveController.prepareNextMove ();
		}
	}

	void OnGUI() {
//		if (player.guiEnable) {
//			var position = player.cam.WorldToScreenPoint(player.parent.gameObject.transform.position); 
//			GUI.Box(new Rect(position.x-20, Screen.height-position.y-10, 40, 20), getCurrentSpeedPercent());
//		}
	}

	private string getCurrentSpeedPercent(){
		if (player.parent != null) {
			return ((int)(player.speed * 100 / player.escapeSpeed)).ToString () + "%";
		} else {
			return "You are free now!";
		}
	}


}

